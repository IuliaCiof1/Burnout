using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorCursor : MonoBehaviour
{
    //RectTransform cursorRect;
    //[SerializeField] RectTransform monitorRect;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    cursorRect = GetComponent<RectTransform>();

    //    Cursor.visible = true;
        
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    Vector3[] corners = new Vector3[4];
    //    monitorRect.GetWorldCorners(corners);
    //    mousePos.x = Mathf.Clamp(mousePos.x, corners[0].x, corners[3].x);

    //    //cursorRect.position = Input.mousePosition;

    //    //RectTransformUtility.ScreenPointToWorldPointInRectangle(
    //    //    canvas.GetComponent<RectTransform>(), Input.mousePosition, eventData.pressEventCamera, out var globalMousePos))
    //}


    //void Update()
    //{
    //    // Convert the bounds of the monitor RectTransform to screen space
    //    Vector3[] corners = new Vector3[4];
    //    monitorRect.GetWorldCorners(corners);
    //    Vector2 minScreenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, corners[0]); // Bottom-left corner
    //    Vector2 maxScreenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, corners[2]); // Top-right corner

    //    // Get the current mouse position in screen space
    //    Vector2 mousePosition = Input.mousePosition;

    //    // Clamp the cursor's screen position to the monitor's screen bounds
    //    float clampedX = Mathf.Clamp(mousePosition.x, minScreenPos.x, maxScreenPos.x);
    //    float clampedY = Mathf.Clamp(mousePosition.y, minScreenPos.y, maxScreenPos.y);

    //    // If the cursor is outside bounds, set it back within the bounds
    //    if (mousePosition.x != clampedX || mousePosition.y != clampedY)
    //    {
    //        Cursor.SetCursorPosition((int)clampedX, (int)clampedY);
    //    }
    //}


    [SerializeField] private RectTransform monitorRect;
    [SerializeField] private RectTransform cursorRect;

    void Start()
    {
        //// Hide the system cursor
        Cursor.visible = false;
    }

    void Update()
    {

        //this might not work when the monitor is rotate 90 degrees
        Vector3 mousePos;

        // This converts the screen space position to a world position relative to monitorRect
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            monitorRect, Input.mousePosition, Camera.main, out mousePos);

        //Debug.Log("Converted Mouse World Position: x:" + mousePos.x+" y: "+ mousePos.y + " z: "+mousePos.z);

        // Clamp mouse position to stay within monitor bounds
        Vector3[] corners = new Vector3[4];
        monitorRect.GetWorldCorners(corners);

        float clampedX = Mathf.Clamp(mousePos.z, corners[0].z, corners[2].z);
        float clampedY = Mathf.Clamp(mousePos.y, corners[0].y, corners[1].y);

        //print(clampedX + "  " +corners[0] +" "+corners[2]);

        // Update cursorRect position to the clamped position
        cursorRect.position = new Vector3(cursorRect.position.x, clampedY, clampedX);
        // cursorRect.localPosition = cursorRect.parent.InverseTransformPoint(new Vector3(cursorRect.position.x, clampedY, clampedX));

        //cursorRect.SetAsLastSibling();
    }
}
