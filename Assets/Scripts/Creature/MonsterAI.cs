using System.Collections;
using UnityEngine;
using UnityEngine.AI; // For using NavMeshAgent

public class MonsterAI : MonoBehaviour
{
    public float wanderRadius = 10f; // How far the monster will wander from its current position
    public float wanderInterval = 5f; // How often the monster chooses a new random point
    public float wanderSpeed = 2f;
    public float chaseSpeed = 5f;
    public float hearingRange = 15f;
    public float closeRange = 5f;
    public NavMeshAgent agent;
    public Transform player;

    private bool isChasing = false;
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

        // Check if the player is within hearing range
        if (distanceToPlayer < hearingRange)
        {
            // If the player is crouching and far enough away, the monster should not hear the player
            if (playerController.isCrouching && distanceToPlayer > closeRange)
            {
                Debug.Log("Player is crouching and far. Monster does not hear.");
                isChasing = false;
                agent.speed = wanderSpeed;
            }
            else if (playerController.canMove && (distanceToPlayer <= closeRange || !playerController.isCrouching))
            {
                // If the player is running or moving within close range, the monster hears them
                Debug.Log("Player detected! Monster is chasing.");
                isChasing = true;
                agent.speed = chaseSpeed;
            }
            else
            {
                Debug.Log("Player is out of hearing range or making no noise.");
                isChasing = false;
                agent.speed = wanderSpeed;
            }
        }
        else
        {
            Debug.Log("Player is out of hearing range.");
            isChasing = false;
            agent.speed = wanderSpeed;
        }
    }
}