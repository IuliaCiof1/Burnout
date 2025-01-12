using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CH1_Moving : State
{
    public override void Enter(CH1_Cockroach bug)
    {
        bug.SetRandomDirection();
    }

    public override void Update(CH1_Cockroach bug)
    {
        bug.ApplyFlockingBehavior();
        bug.SetMovingVisual();
        bug.speed = 0.4f;
        bug.Move();

        if (bug.IsPlayerNearby())
        {
            if (!bug.isAggresive || GlobalStateManager.HasKey(0))
                bug.ChangeState(new CH1_Fleeing());
            else
                bug.ChangeState(new CH1_Attack());
        }
        else if (UnityEngine.Random.value < 0.02f)
            bug.ChangeState(new CH1_Resting());
    }

    public override void Exit(CH1_Cockroach bug) { /* Optional cleanup */ }
}
