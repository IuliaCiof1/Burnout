using System.Collections;
using UnityEngine;

public class CH1_Attack : State
{
    private float chaseSpeed = 0.6f;
    private float maxChaseDistance = 10f;
    private float obstacleAvoidanceRange = 1.5f;
    private float attackRange = 5f;

    public override void Enter(CH1_Cockroach bug)
    {
        bug.SetAttackVisuals();
    }

    public override void Update(CH1_Cockroach bug)
    {
        if (GlobalStateManager.HasKey(0))
        {
            bug.ChangeState(new CH1_Moving());
            return;
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(bug.transform.position, player.transform.position);

        if (distanceToPlayer > maxChaseDistance)
        {
            bug.ChangeState(new CH1_Moving());
            return;
        }

        if (distanceToPlayer <= attackRange)
        {
            //SoundFXManager.instance.PlaySoundFXClip(bug.AttackingSound, bug.transform, 0.01f); ASTA nu trebuie sa se intample la fiecare frame
        }

        Vector3 directionToPlayer = (player.transform.position - bug.transform.position).normalized;

        Vector3 adjustedDirection = AvoidObstacles(bug, directionToPlayer);

        bug.SetAttackVisuals();
        bug.direction = adjustedDirection;
        bug.speed = chaseSpeed;
        bug.Move();
    }

    public override void Exit(CH1_Cockroach bug)
    {

    }

    private Vector3 AvoidObstacles(CH1_Cockroach bug, Vector3 targetDirection)
    {
        float avoidanceStrength = 0.02f;
        Vector3 avoidanceVector = Vector3.zero;

        Collider[] nearbyObjects = Physics.OverlapSphere(bug.transform.position, obstacleAvoidanceRange);

        foreach (var obj in nearbyObjects)
        {
            if (obj.CompareTag("CockRoach") && obj.gameObject != bug.gameObject)
            {
                Vector3 directionAway = bug.transform.position - obj.transform.position;
                avoidanceVector += directionAway.normalized / directionAway.magnitude;
            }
        }

        Vector3 combinedDirection = targetDirection + avoidanceVector * avoidanceStrength;

        return combinedDirection.normalized;
    }

}
