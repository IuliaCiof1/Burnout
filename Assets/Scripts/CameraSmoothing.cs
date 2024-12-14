using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothing : MonoBehaviour
{
    [SerializeField] private Transform headTransform; // The animated head
    [SerializeField] private Transform headAnchor; // The parent of the camera
    [SerializeField] private float smoothing = 0.2f;

    void LateUpdate()
    {
        // Smoothly follow the head position
       headAnchor.position = headTransform.position;
       
        // Match the head rotation but apply less bobbing
        headAnchor.rotation = Quaternion.Lerp(headAnchor.rotation, headTransform.rotation, smoothing);
      
    }
}
