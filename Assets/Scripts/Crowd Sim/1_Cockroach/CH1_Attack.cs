using System.Collections;
using UnityEngine;

public class CH1_Attack : State
{
    private float chaseSpeed = 0.6f;
    private float maxChaseDistance = 10f;
    private float obstacleAvoidanceRange = 1.5f;

    public override void Enter(CH1_Cockroach bug)
    {
        bug.SetMovingVisual();
    }

    public override void Update(CH1_Cockroach bug)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(bug.transform.position, player.transform.position);

        if (distanceToPlayer > maxChaseDistance)
        {
            bug.ChangeState(new CH1_Moving());
            return;
        }

        Vector3 directionToPlayer = (player.transform.position - bug.transform.position).normalized;

        Vector3 adjustedDirection = AvoidObstacles(bug, directionToPlayer);

        bug.SetMovingVisual();
        bug.direction = adjustedDirection;
        bug.speed = chaseSpeed; 
        bug.Move();
    }

    public override void Exit(CH1_Cockroach bug)
    {
    }

    private Vector3 AvoidObstacles(CH1_Cockroach bug, Vector3 targetDirection)
    {
        RaycastHit hit;
        if (Physics.Raycast(bug.transform.position, targetDirection, out hit, obstacleAvoidanceRange))
        {
            if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
            {
                Vector3 avoidDirection = Vector3.Reflect(targetDirection, hit.normal);
                return avoidDirection.normalized;
            }
        }

        return targetDirection;
    }
}
