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

    private void Start()
    {
        camera = Camera.main;
    }

    public void Interact()
    {
        //lock camera potision and rotation
        LockCamera();

        //lock character movement
    }

    void LockCamera()
    {
        playerController.enabled = false;

        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(camera.transform.DOLocalRotateQuaternion(Quaternion.identity, duration));
        sequence.Join(player.transform.DOLookAt(rotateToSitPoint.position, duration).SetRelative(false));
        sequence.Append(player.transform.DOMove(monitorCamera.position, duration).SetRelative(false));
        //sequence.Join(camera.transform.DORotateQuaternion(Quaternion.identity, duration));
        //player.transform.DOLookAt(new Vector3(player.transform.position.x, lookAtPoint.position.y, player.transform.position.z), duration);
       sequence.Join(player.transform.DOLookAt(lookAtPoint.position, duration).SetRelative(false));
        //Sequence sequence = DOTween.Sequence();

        ////sequence.Append(player.transform.DOMove(monitorCamera.position, duration));
        //sequence.Append(player.transform.DOLookAt(lookAtPoint.position, duration));
        ////sequence.Append(player.transform.DORotateQuaternion(monitorCamera.transform.rotation, duration));
        sequence.Join(camera.DOFieldOfView(monitorCamera.GetComponent<Camera>().fieldOfView, duration).SetRelative(false));


        Cursor.lockState = CursorLockMode.None;

        ////player.transform.DOMove(monitorCamera.position, duration);
        ////player.transform.DOLookAt(lookAtPoint.position, duration);
        ////print(player.transform.position);
        ////camera.DOFieldOfView(monitorCamera.GetComponent<Camera>().fieldOfView, duration);
    }

    void LockCharacter()
    {

    }
}
