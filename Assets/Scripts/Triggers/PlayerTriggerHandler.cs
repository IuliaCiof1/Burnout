using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TriggerZone []triggerZones = other.GetComponents<TriggerZone>();

        for (int i = 0; i < triggerZones.Length; i++)
        {
            if (triggerZones[i] != null)
            {
                triggerZones[i].Trigger();

            }
            //Destroy(other.gameObject);
            //triggerZones[i].enabled = false;
        }
            if (triggerZones.Length!=0)
                Destroy(other.gameObject);

    }
}
