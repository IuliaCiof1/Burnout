using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    #region - Declarations
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;
    //public float batteryLife = 100f; Putem implementa o functionalitate de baterie or something
    //public float drainRate = 1f;
    //public float rechargeRate = 5f;
    private bool isOn = false;
    #endregion

    #region - Methods
    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponent<Light>();
        }

        flashlight.enabled = isOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
    #endregion
}
