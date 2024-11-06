using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MONITOR_Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;
    [SerializeField] Transform monitorCamera;
    [SerializeField] float duration;
    [SerializeField] Transform lookAtPoint;
    [SerializeField] Transform rotateToSitPoint;
    [SerializeField] Controller playerController;
    Camera camera;

    Vector3 oldPlayerPos;
    //Vector3 oldPlayerPos;
    float fieldOfView;

    private void Start()
    {
        camera = Camera.main;
        fieldOfView = camera.fieldOfView;
        
    }

    public void Interact()
    {
        SitAtComputer();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetUp();
        }
    }

    void SitAtComputer()
    {
        oldPlayerPos = player.transform.position;
        playerController.enabled = false;

        Sequence sequence = DOTween.Sequence();

        // Move the player to the sit position, then rotate the player to face the monitor
        sequence.Append(player.transform.DOLookAt(rotateToSitPoint.position, duration))
            .Append(player.transform.DOMove(monitorCamera.position, duration))
                // After the player is positioned, rotate the camera to look at the monitor
                .Append(camera.transform.DOLookAt(lookAtPoint.position, duration))
                .Join(camera.DOFieldOfView(monitorCamera.GetComponent<Camera>().fieldOfView, duration));

    }

    void GetUp()
    {
        playerController.enabled = true;

        Sequence sequence = DOTween.Sequence();
        camera.transform.SetParent(null, true);
        sequence
           .Append(camera.DOFieldOfView(fieldOfView, duration))

           // Step 2: Move the player to the sit position and rotate to face the monitor
           .Append(player.transform.DOMove(oldPlayerPos, duration))
           .Join(player.transform.DOLookAt(rotateToSitPoint.position, duration))
            .OnComplete(() =>
            {
                camera.transform.SetParent(player.transform, true);
               
            }); ;

        //camera.fieldOfView = fieldOfView;
    }

   
}
