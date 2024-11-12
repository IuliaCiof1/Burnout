using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderInterval = 5f;
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

        if (distanceToPlayer < hearingRange)
        {
            if (playerController.isCrouching && distanceToPlayer > closeRange)
            {
                isChasing = false;
                agent.speed = wanderSpeed;
            }
            else
            {
                isChasing = true;
                agent.speed = chaseSpeed;
            }
        }
        else
        {
            isChasing = false;
            agent.speed = wanderSpeed;
        }
    }
}
