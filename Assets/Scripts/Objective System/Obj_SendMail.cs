using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objectives/SendMail")]
public class Obj_SendMail : Objective
{

    public override void ActivateObjective()
    {

        ObjectiveEvents.OnEmailSent += Complete;
        Debug.Log("Objective started: " + description);

    }

   

    public override void DeactivateObjective()
    {
        ObjectiveEvents.OnEmailSent -= Complete;
        Debug.Log("Objective completed: " + description);
    }

}
