using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective : ScriptableObject
{
    public string description;
    public bool isCompleted;

    public abstract void ActivateObjective();
    public abstract void DeactivateObjective();

    protected void Complete()
    {
        ObjectiveEvents.ObjectiveCompleted(this); // Notify system that this objective is complete
    }
}
