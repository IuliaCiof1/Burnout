using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseLockDoor : TriggerZone
{
    [SerializeField] DOOR_Interacting door;

    public override void  Trigger()
    {
        if (door.isOpen)
            door.Interact();

        door.isLocked = true;
    }
}
