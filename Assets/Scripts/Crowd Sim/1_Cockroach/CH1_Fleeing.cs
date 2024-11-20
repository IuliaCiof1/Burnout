using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH1_Fleeing : State
{
    public override void Enter(CH1_Cockroach bug)
    {
        bug.SetFleeDirection();
    }

    public override void Update(CH1_Cockroach bug)
    {
        bug.SetMovingVisual();
        bug.speed = 0.6f;
        bug.Move();

        if (!bug.IsPlayerNearby())
            bug.ChangeState(new CH1_Moving());
    }

    public override void Exit(CH1_Cockroach bug) { /* Optional cleanup */ }
}