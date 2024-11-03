using System.Collections;
using UnityEngine;

public class SWITCH_Interacting : MonoBehaviour, IInteractable
{
    private Light[] roomLights;

    private void Start()
    {
        roomLights = GetComponentsInChildren<Light>();
    }

    public void Interact()
    {
        ToggleLights();
    }

    private void ToggleLights()
    {
        foreach (Light light in roomLights)
        {
            light.enabled = !light.enabled;
        }
    }
}
