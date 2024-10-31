using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Transform parent = transform.parent;


        for(int i=7; i>0; i--)  //using this instead of infinite loop is safer
        {
            if(parent.TryGetComponent<Canvas>(out canvas))
            {
                break;
            }
        }

        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        // Capture the offset between the mouse position and the window's position
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform, eventData.position, eventData.pressEventCamera, out var worldMousePos);
        offset = rectTransform.position - worldMousePos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Convert the mouse position to a world position, then apply the offset
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var globalMousePos))
        {
            rectTransform.position = globalMousePos + offset;
        }
    }

   
}
