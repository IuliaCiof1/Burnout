using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothing : MonoBehaviour
{
    [SerializeField] private Transform headTransform; // The animated head
    [SerializeField] private Transform headAnchor; // The parent of the camera
    [SerializeField] private float smoothing = 0.2f;

    bool canSmoothing=true;
    Quaternion rotation;

    void LateUpdate()
    {

        if (canSmoothing)
        {
             // Smoothly follow the head position
            headAnchor.position = headTransform.position;

            // Match the head rotation but apply less bobbing
            headAnchor.rotation = Quaternion.Lerp(headAnchor.rotation, headTransform.rotation, smoothing);
        }
        else
        {
            headAnchor.position = headTransform.position;
                //new Vector3(headAnchor.position.x, headTransform.position.y, headAnchor.position.z);
        }
    }

    public void NoSmoothing()
    {
        rotation = transform.rotation;
        canSmoothing = false;    
    }
}
