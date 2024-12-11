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
            lightFlicker.StopLights();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (lightFlicker != null)
        {
            Debug.Log("Mouse Left: " + gameObject.name);
            lightFlicker.TurnOffLight();
            lightFlicker.StartFlickering();
        }
    }
}
