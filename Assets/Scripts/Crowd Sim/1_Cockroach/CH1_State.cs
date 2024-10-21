using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter(CH1_Cockroach bug);
    public abstract void Update(CH1_Cockroach bug);
    public abstract void Exit(CH1_Cockroach bug);
}
