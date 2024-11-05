using System.Collections;
using UnityEngine;

public class SWITCH_Interacting : MonoBehaviour, IInteractable
{
    private Light[] roomLights;
    private LightFlicker flicker;

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
            if (light.TryGetComponent<LightFlicker>(out flicker))
            {
                if (flicker.enabled)
                    flicker.StopLights();
                else
                    flicker.StartLights();
                flicker.enabled = !flicker.enabled;
            }

            light.enabled = !light.enabled;
        }
    }
}
