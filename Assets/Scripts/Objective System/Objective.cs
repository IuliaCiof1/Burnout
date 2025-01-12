using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective")]
public class Objective : ScriptableObject
{
    public string description;
    public bool isCompleted;

    //public abstract void ActivateObjective();
    //public abstract void DeactivateObjective();

    protected void Complete()
    {
        isCompleted = true;
        ObjectiveEvents.ObjectiveCompleted(this); // Notify system that this objective is complete
    }


    public string objectiveEventName;

    public void ActivateObjective()
    {
        ObjectiveEvents.SubscribeEvent(objectiveEventName, Complete);
        //ObjectiveEvents.OnOpenSpookyMail += Complete;
        Debug.Log("Objective started: " + description);

    }



    public void DeactivateObjective()
    {
        //ObjectiveEvents.OnOpenSpookyMail -= Complete;
        ObjectiveEvents.UnsubscribeEvent(objectiveEventName, Complete);
        Debug.Log("Objective completed: " + description);
    }
}
