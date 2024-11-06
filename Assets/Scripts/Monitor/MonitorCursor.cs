using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorCursor : MonoBehaviour
{
   
    [SerializeField] private RectTransform monitorRect;
    [SerializeField] private RectTransform cursorRect;
    [SerializeField] private Vector3 cursorOffset;

    void Start()
    {
        //// Hide the system cursor
        Cursor.visible = false;
    }

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        // Get the mouse position in screen space
        Vector2 mousePos = Input.mousePosition;

        // Convert the screen space mouse position to local space of the monitor
        RectTransformUtility.ScreenPointToLocalPointInRectangle(monitorRect, mousePos, Camera.main, out Vector2 localMousePos);

        // Get the bounds of the monitor RectTransform in local coordinates
        Vector3[] corners = new Vector3[4];
        monitorRect.GetLocalCorners(corners);

        // Clamp mouse position to stay within monitor bounds
        float clampedX = Mathf.Clamp(localMousePos.x, corners[0].x, corners[2].x);
        float clampedY = Mathf.Clamp(localMousePos.y, corners[0].y, corners[1].y);

        // Update cursorRect position to the clamped position plus offset
        cursorRect.localPosition = new Vector3(clampedX + cursorOffset.x, clampedY + cursorOffset.y, cursorRect.localPosition.z);
    }
}
