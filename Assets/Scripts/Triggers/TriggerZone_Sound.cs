using UnityEngine;

public class TriggerZone_Sound : TriggerZone
{
    [SerializeField]private AudioClip sound;

    public override void Trigger()
    {
        
        print("trigger dialog");
        triggerID = 3;
        ActionManager.Instance.HandleTrigger(triggerID, null, sound, null);
    }
}
