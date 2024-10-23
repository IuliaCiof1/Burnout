using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Range(0.01f, 1f)]
    public float TurnOffChance;
    [Range(0.025f, 0.25f)]
    public float TurnOffRollInterval;

    private Light SelfLightComponent;

    // Start is called before the first frame update
    void Start()
    {
        SelfLightComponent = gameObject.GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Flicker()
    {
        WaitForSecondsRealtime CheckInterval = new WaitForSecondsRealtime(TurnOffRollInterval);
        while (true)
        {
            float Randy = Random.Range(0f, 1f);
            if (Randy < TurnOffChance)
            {
                SelfLightComponent.enabled = false;
            }
            else
            {
                SelfLightComponent.enabled = true;
            }
            yield return CheckInterval;
        }
    }
}