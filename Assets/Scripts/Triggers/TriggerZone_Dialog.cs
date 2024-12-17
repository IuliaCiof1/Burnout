using UnityEngine;

public class TriggerZone_Dialog : TriggerZone
{
    [SerializeField][TextArea] private string description;
    [SerializeField]private AudioClip sound;

    public override void Trigger()
    {
        triggerID = 1;
        ActionManager.Instance.HandleTrigger(triggerID, description, sound, null);
    }
}
