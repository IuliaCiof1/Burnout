using System.Collections;
using UnityEngine;

public class LightFlicker2 : MonoBehaviour
{
    [Range(0.01f, 1f)]
    public float TurnOffChance;
    [Range(0.025f, 0.25f)]
    public float TurnOffRollInterval;
    public bool canFlicker = true;
    private Light SelfLightComponent;

    private Coroutine flickerCoroutine;

    void Start()
    {
        SelfLightComponent = gameObject.GetComponent<Light>();
        flickerCoroutine = StartCoroutine(Flicker());
    }

    public IEnumerator Flicker()
    {
        WaitForSecondsRealtime CheckInterval = new WaitForSecondsRealtime(TurnOffRollInterval);

        while (true)
        {
            if (canFlicker)
            {
                float Randy = Random.Range(0f, 1f);
                if (Randy < TurnOffChance)
                {
                    SelfLightComponent.enabled = true;
                }
                else
                {
                    SelfLightComponent.enabled = false;
                }
            }
            yield return CheckInterval;
        }
    }

    public void StopLights()
    {
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }
        SelfLightComponent.enabled = true; // Ensure light stays on
    }

    public void StartFlickering()
    {
        if (flickerCoroutine == null)
        {
            flickerCoroutine = StartCoroutine(Flicker());
        }
        canFlicker = true; // Enable flickering
    }

    public void TurnOffLight()
    {
        canFlicker = false; // Stop flickering
        SelfLightComponent.enabled = false; // Ensure light is off
    }
}
