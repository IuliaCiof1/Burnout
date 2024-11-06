using System.Collections;
using UnityEngine;

public class CH1_Resting : State
{
    private float restDuration;
    private float restTimer;

    public override void Enter(CH1_Cockroach bug)
    {
        restDuration = UnityEngine.Random.Range(1f, 3f);
        restTimer = restDuration;
        bug.SetResting(true);  
    }

    public override void Update(CH1_Cockroach bug)
    {
        restTimer -= Time.deltaTime;
        if (restTimer <= 0)
        {
            bug.ChangeState(new CH1_Moving());
        }
    }

    public override void Exit(CH1_Cockroach bug)
    {
        bug.SetResting(false);
    }
}
