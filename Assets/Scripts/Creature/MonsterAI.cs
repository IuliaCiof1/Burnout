using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    #region - Declarations
    public float wanderRadius = 10f;
    public float wanderInterval = 5f;
    public float wanderSpeed = 2f;
    public float chaseSpeed = 5f;
    public float hearingRange = 10f;
    public float closeRange = 3f;
    public float investigateRadius = 3f;
    public float investigateTime = 5f;
    public float safeDistanceFromPlayer = 2.5f;
    public NavMeshAgent agent;
    public Transform player;
    public Controller playerController;

    private bool isChasing = false;
    private bool isInvestigating = false;
    private float investigateEndTime = 0f;
    private Vector3 lastHeardPosition;
    private float nextWanderTime = 0f;
    #endregion

    #region - Methods
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = wanderSpeed;
        ChooseRandomDestination();
    }

    void Update()
    {
        if (isChasing)
        {
            agent.SetDestination(player.position);
        }
        else if (isInvestigating)
        {
            Investigate();
        }
        else
        {
            if (Time.time >= nextWanderTime)
            {
                ChooseRandomDestination();
                nextWanderTime = Time.time + wanderInterval;
            }

            DetectPlayer();
        }
    }

    void ChooseRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(navHit.position);
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //Controller playerController = player.GetComponent<Controller>();

        if (distanceToPlayer < closeRange && playerController.isMoving)
        {
            isChasing = true;
            agent.speed = chaseSpeed;
            isInvestigating = false;
        }
        else if (distanceToPlayer < hearingRange)
        {
            if (playerController.isCrouching && !playerController.isMoving)
            {
                isChasing = false;
                agent.speed = wanderSpeed;
            }
            else if (playerController.isMoving)
            {
                lastHeardPosition = player.position;
                isChasing = false;
                isInvestigating = true;
                investigateEndTime = Time.time + investigateTime;
                agent.speed = wanderSpeed;
                agent.SetDestination(lastHeardPosition + Random.insideUnitSphere * investigateRadius);
            }
        }
    }

    void Investigate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= safeDistanceFromPlayer)
        {
            // If too close to the player, stop moving or move away slightly
            Vector3 awayFromPlayer = (transform.position - player.position).normalized * safeDistanceFromPlayer;
            Vector3 safePosition = player.position + awayFromPlayer;
            agent.SetDestination(safePosition);
        }
        else if (Time.time >= investigateEndTime)
        {
            isInvestigating = false;
            ChooseRandomDestination();
        }
        else if (Vector3.Distance(transform.position, lastHeardPosition) <= investigateRadius)
        {
            agent.SetDestination(lastHeardPosition + Random.insideUnitSphere * investigateRadius);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        TriggerScreamer(other);
    }

    public void TriggerScreamer(Collider other = null)
    {
        if (other.CompareTag("Player"))
        {
            agent.isStopped = true;
            playerController.canMove = false;
        }
        Debug.Log("Screamer activated!");
    }

    public void EndGameCinematic()
    {
        agent.isStopped = true;
        playerController.canMove = false;
    }
    #endregion
}
