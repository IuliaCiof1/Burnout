using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objectives/OpenSpookyMail")]
public class Obj_OpenSpookyMail : Objective
{
    public string objectiveEventName;

    public override void ActivateObjective()
    {
        ObjectiveEvents.SubscribeEvent(objectiveEventName, Complete);
        //ObjectiveEvents.OnOpenSpookyMail += Complete;
        Debug.Log("Objective started: " + description);

    }

   

    public override void DeactivateObjective()
    {
        //ObjectiveEvents.OnOpenSpookyMail -= Complete;
        ObjectiveEvents.UnsubscribeEvent(objectiveEventName, Complete);
        Debug.Log("Objective completed: " + description);
    }

}
