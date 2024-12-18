using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public KeyCode toggleKey = KeyCode.F;
    public KeyCode UVtoggleKey = KeyCode.U;

    private bool isOn = false;
    public bool hasUV = false;



    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponent<Light>();
        }

        flashlight.enabled = isOn;
        enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }

    public void EnableFlashlight()
    {
        enabled = true;
        flashlight.enabled = isOn;
    }
}
