using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        bug.Move();

        if (bug.IsPlayerNearby())
            bug.ChangeState(new CH1_Fleeing());
    }

    public override void Exit(CH1_Cockroach bug) { /* Optional cleanup */ }
}
