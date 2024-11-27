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

    public static bool isSitting;

    [SerializeField] private Phone phone;



    private void Awake()
    {
        
        camera = Camera.main;
        fieldOfView = camera.fieldOfView;
        StartState();

    }

    public void Interact()
    {
        SitAtComputer();
    }


    private void Update()
    {
        if (isSitting)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GetUp();
            }
        }
    }

    void StartState()
    {
        print("first");
        isSitting = true;

        oldPlayerPos = player.transform.position;
        playerController.enabled = false;

        Sequence sequence = DOTween.Sequence();

        // Move the player to the sit position, then rotate the player to face the monitor
        player.transform.LookAt(rotateToSitPoint.position);
        player.transform.position = monitorCamera.position;
        // After the player is positioned, rotate the camera to look at the monitor
        camera.transform.LookAt(lookAtPoint.position);
        camera.fieldOfView = monitorCamera.GetComponent<Camera>().fieldOfView;
    }

    void SitAtComputer()
    {
        phone.HidePhone();

        isSitting = true;

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
        isSitting = false;

        // Disable player control initially
        playerController.enabled = true;

        // Start a DOTween sequence
        Sequence sequence = DOTween.Sequence();

        // Step 1: Temporarily unparent camera to change its field of view independently
        //camera.transform.SetParent(null, true);

        // Step 2: Apply field of view change while the camera is detached
        sequence.Append(camera.DOFieldOfView(fieldOfView, duration))

                // Step 3: Reparent the camera to the player so it moves along with the player
                //.AppendCallback(() => camera.transform.SetParent(player.transform, true))

                // Step 4: Move the player to the sit position and rotate to face the monitor
                .Append(player.transform.DOMove(oldPlayerPos, duration))
               

                // Step 5: Re-enable player control after the sequence completes
                .Append(player.transform.DOLocalRotateQuaternion(Quaternion.identity, duration));

        // Unlock the cursor for interaction
        Cursor.lockState = CursorLockMode.None;
        //camera.fieldOfView = fieldOfView;

        phone.ShowPhone();
    }

   
}
