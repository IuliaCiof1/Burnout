using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TriggerZone triggerZone = other.GetComponent<TriggerZone>();
        if (triggerZone != null)
        {
            ActionManager.Instance.HandleTrigger(triggerZone.triggerID, triggerZone.description, triggerZone.sound);
            GameObject.Destroy(triggerZone.gameObject);
        }
    }
}
