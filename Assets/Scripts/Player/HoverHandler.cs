using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private LightFlicker2 lightFlicker;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (lightFlicker != null)
        {
            Debug.Log("Mouse Hovered Over: " + gameObject.name);
            lightFlicker.canFlicker = false;
            lightFlicker.StartLights();
        }
        else
        {
            Debug.LogWarning("LightFlicker2 reference is missing!");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (lightFlicker != null)
        {
            Debug.Log("Mouse Left: " + gameObject.name);
            lightFlicker.canFlicker = true;
            lightFlicker.StopLights();
        }
        else
        {
            Debug.LogWarning("LightFlicker2 reference is missing!");
        }
    }
}
