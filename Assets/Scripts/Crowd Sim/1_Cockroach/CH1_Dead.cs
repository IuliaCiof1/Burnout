using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH1_Dead : State
{
    public override void Enter(CH1_Cockroach bug)
    {
        bug.SetDeadVisual();
        bug.DisableMovement();
        bug.direction = Vector3.zero;
    }

    public override void Update(CH1_Cockroach bug) { }

    public override void Exit(CH1_Cockroach bug) { }
}
