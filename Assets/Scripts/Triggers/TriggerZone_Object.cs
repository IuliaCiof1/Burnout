using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone_Object : TriggerZone
{
 
    [SerializeField] List<GameObject> triggeredObjects;

    [Tooltip("2 - show the object; 6 - hide the object")]
    [SerializeField] int triggerID_;
   
    public override void Trigger()
    {
        print("trigger object");
        triggerID = triggerID_;
        ActionManager.Instance.HandleTrigger(triggerID, null, null, triggeredObjects);

    }
}
