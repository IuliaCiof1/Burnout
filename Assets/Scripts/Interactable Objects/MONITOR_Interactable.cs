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
    [SerializeField] Transform chair;
    [SerializeField] Controller playerController;
    Camera camera;

    float fieldOfView;

    public static bool isSitting;
    private bool isAtChair;

    [SerializeField] private Phone phone;
    Sequence sequence;

    private Animator animator;
    private Quaternion lastCameraRotation;

    private void Start()
    {
        animator = player.GetComponentInChildren<Animator>();

        camera = Camera.main;
        fieldOfView = camera.fieldOfView;
        StartState();
        //SitAtComputer();
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
        lastCameraRotation = Quaternion.identity;
        phone.HidePhone();
        playerController.canMove = false;
        playerController.enabled = false;
        isSitting = true;

        sequence = DOTween.Sequence();   //Start DOTween sequence

        // Calculate the duration based on the player's speed
        float distance = Vector3.Distance(player.transform.position, monitorCamera.position);
        float durationMove = distance / 1.5f;


        sequence
            .Append(player.transform.DOMove(monitorCamera.position, 0))  //Move player to chair
                                                                         //Trigger the walking animation
            .AppendCallback(() => { animator.Play("Sit", 0, 2); })          //Trigger the sit down animation
            .Append(player.transform.DOLookAt(rotateToSitPoint.position, 0, AxisConstraint.Y)) //rotate player to sitting rotation
            .AppendCallback(() => { player.transform.parent = chair; })              //parent player to chair
            .Append(chair.transform.DOLookAt(lookAtPoint.position, 0, AxisConstraint.Y)) // Rotate the chair after the animation finishes
            .Join(player.transform.GetChild(0).DOLocalMove(new Vector3(0, -0.7920046f, 0), 0)) //Resets the position otherwise things get messy
            .Join(camera.DOFieldOfView(monitorCamera.GetComponent<Camera>().fieldOfView, 0));  //changes the fieldof view to zoom in on computer

        camera.gameObject.transform.localEulerAngles = new Vector3(340.845215f, 12.969347f, 351.18396f); //Look at computer

        Cursor.lockState = CursorLockMode.None;
    }

    void SitAtComputer()
    {
        lastCameraRotation = camera.gameObject.transform.localRotation;
        print(lastCameraRotation);
        phone.HidePhone();
        playerController.canMove = false;
        playerController.enabled = false;
        isSitting = true;

        sequence = DOTween.Sequence();   //Start DOTween sequence

        // Calculate the duration based on the player's speed
        float distance = Vector3.Distance(player.transform.position, monitorCamera.position);
        float durationMove = distance / 1.5f;


        sequence
            .Append(player.transform.DOMove(monitorCamera.position, durationMove))  //Move player to chair
            .JoinCallback(() => { PlayAnimation(); })                                //Trigger the walking animation
            .AppendCallback(() => { animator.SetBool("isAtChair", true); })          //Trigger the sit down animation
            .Append(player.transform.DOLookAt(rotateToSitPoint.position, duration, AxisConstraint.Y)) //rotate player to sitting rotation
            .AppendCallback(() => { player.transform.parent = chair; })              //parent player to chair
            .AppendInterval(1f)                                                     //wait until animation finishes
            .Append(chair.transform.DOLookAt(lookAtPoint.position, duration, AxisConstraint.Y)) // Rotate the chair after the animation finishes
            .Join(camera.transform.DOLookAt(lookAtPoint.position, duration))       //Look at computer
            .Join(player.transform.GetChild(0).DOLocalMove(new Vector3(0, -0.7920046f, 0), 2)) //Resets the position otherwise things get messy
            .Join(camera.DOFieldOfView(monitorCamera.GetComponent<Camera>().fieldOfView, duration));  //changes the fieldof view to zoom in on computer

        Cursor.lockState = CursorLockMode.None;
    }

    void PlayAnimation()
    {
        // Trigger the sitting animation
        animator.SetBool("isSitting", isSitting);
    }


    void GetUp()
    {
        isSitting = false;
 
        Sequence sequence = DOTween.Sequence();

        float distance = Vector3.Distance(player.transform.position, monitorCamera.position);
        float durationMove = distance / 1.5f;

        sequence
            .Join(camera.DOFieldOfView(fieldOfView, duration))
            .Append(chair.transform.DOLookAt(rotateToSitPoint.position, duration, AxisConstraint.Y))
            .AppendCallback(() =>
            {
                //playerController.GetComponent<CharacterController>().enabled = false;

                //  float posY = playerController.transform.localPosition.y;
                // playerController.transform.localPosition = new Vector3(camera.gameObject.transform.position.x, posY, camera.gameObject.transform.position.z);

                // playerController.GetComponent<CharacterController>().enabled = true;

                player.transform.parent = playerController.transform;

            })
             //.Append(player.transform.DOLookAt(rotateToSitPoint.position, duration, AxisConstraint.Y))


             .JoinCallback(() =>
             {
                 PlayAnimation();
                 phone.ShowPhone();
             })


             .AppendInterval(2f)
             .Append(player.transform.DOLocalMove(new Vector3(0, 0, 0), 1))
             .Join(player.transform.GetChild(0).DOLocalMove(new Vector3(0, -0.7920046f, 0), 1.5f))
             .Append(player.transform.DOLocalRotateQuaternion(Quaternion.identity, duration))
             .Join(camera.gameObject.transform.DOLocalRotateQuaternion(lastCameraRotation, duration))
             
             .AppendCallback(() =>
             {
                 playerController.enabled = true;
                 playerController.canMove = true;
             });
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
