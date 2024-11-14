using System.Collections;
using UnityEngine;
using UnityEngine.AI; // For using NavMeshAgent

public class MonsterAI : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderInterval = 5f;
    public float wanderSpeed = 2f;
    public float chaseSpeed = 5f;
    public float hearingRange = 15f;
    public float closeRange = 2f; // Very close range for instant detection
    public float investigateRadius = 3f; // Radius around the noise location to search
    public float investigateTime = 5f; // Time to investigate before giving up
    public NavMeshAgent agent;
    public Transform player;

    private bool isChasing = false;
    private bool isInvestigating = false;
    private float investigateEndTime = 0f;
    private Vector3 lastHeardPosition;
    private float nextWanderTime = 0f;

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
        Controller playerController = player.GetComponent<Controller>();

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
        if (Time.time >= investigateEndTime)
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
        if (other.CompareTag("Player"))
        {
            Debug.Log("Found You");
            agent.isStopped = true;

            Controller playerController = player.GetComponent<Controller>(); 
            playerController.canMove = false;

            TriggerScreamer();
        }
    }
    void TriggerScreamer()
    {
        Debug.Log("Screamer activated!");
    }
}
