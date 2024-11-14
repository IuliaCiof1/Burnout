using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerZone : MonoBehaviour
{
    protected int triggerID;

    //public TriggerZone(int triggerID)
    //{
    //    this.triggerID = triggerID;
    //}

    public abstract void Trigger() ; 
}
