using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPY_MONITOR_Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;
    [SerializeField] Transform monitorCamera;
    [SerializeField] float duration;
    [SerializeField] Transform lookAtPoint;
    [SerializeField] Transform rotateToSitPoint;
    [SerializeField] Transform chair;
    [SerializeField] Controller playerController;
    Camera camera;

    Vector3 oldPlayerPos;
    //Vector3 oldPlayerPos;
    float fieldOfView;

    public static bool isSitting;

    [SerializeField] private Phone phone;
    Sequence sequence;

    private Animator animator;

    private void Awake()
    {
        animator = player.GetComponentInChildren<Animator>();

        camera = Camera.main;
        fieldOfView = camera.fieldOfView;
        //StartState();

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
        animator.SetBool("isSitting", true);
        //oldPlayerPos = player.transform.position;
        //playerController.enabled = false;

        //Sequence sequence = DOTween.Sequence();

        //// Move the player to the sit position, then rotate the player to face the monitor
        //player.transform.LookAt(rotateToSitPoint.position);
        //player.transform.position = monitorCamera.position;
        //// After the player is positioned, rotate the camera to look at the monitor
        //camera.transform.LookAt(lookAtPoint.position);
        //camera.fieldOfView = monitorCamera.GetComponent<Camera>().fieldOfView;
    }

    void SitAtComputer()
    {
        phone.HidePhone();

        //isSitting = true;

        //oldPlayerPos = player.transform.position;

        playerController.enabled = false;

        //Sequence sequence = DOTween.Sequence();
        ////animator.SetBool("isSitting", true);
        //// Move the player to the sit position, then rotate the player to face the monitor
        //sequence.Append(player.transform.DOLookAt(rotateToSitPoint.position, duration)).Append(player.transform.DOMove(monitorCamera.position, duration)).Append(camera.transform.DOLookAt(lookAtPoint.position, duration))

        //        // After the player is positioned, rotate the camera to look at the monitor

        //        .Join(camera.DOFieldOfView(monitorCamera.GetComponent<Camera>().fieldOfView, duration));

        isSitting = true;


        sequence = DOTween.Sequence();

        float distance = Vector3.Distance(player.transform.position, monitorCamera.position);

        // Calculate the duration based on the player's speed
        float durationMove = distance / 1.5f;

        // Move player to monitor and rotate to sit point
        sequence.Append(player.transform.DOMove(monitorCamera.position, durationMove))
            .Append(player.transform.DOLookAt(rotateToSitPoint.position, duration, AxisConstraint.Y))

        // Trigger the animation and calculate remaining time
        .AppendCallback(() =>
        {
            PlayAnimation();
            player.transform.parent = chair;
        })
        .AppendInterval(2f)
        //// Get the current animation's remaining time
        //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //float remainingTime = stateInfo.length * (1f - stateInfo.normalizedTime);

        //// Add a delay equal to the animation's remaining time
        //sequence.AppendInterval(remainingTime);

        // Rotate the chair after the animation finishes
       .Append(chair.transform.DOLookAt(lookAtPoint.position, duration, AxisConstraint.Y))
       .Join(camera.transform.DOLookAt(lookAtPoint.position, duration))
        .Join(player.transform.GetChild(0).DOLocalMove(new Vector3(0, -0.894f, 0), 2))
       .Join(camera.DOFieldOfView(monitorCamera.GetComponent<Camera>().fieldOfView, duration));
        
        Cursor.lockState = CursorLockMode.None;
    }

    void PlayAnimation()
    {
        // Trigger the sitting animation
        animator.SetBool("isSitting", isSitting);
        
    }
    //    sequence
    //        .Append(player.transform.DOMove(monitorCamera.position, duration))
    //        .Append(player.transform.DOLookAt(rotateToSitPoint.position, duration, AxisConstraint.Y)).onComplete = PlayAnimation;
    //    sequence.AppendInterval(10f).Append(chair.transform.DOLookAt(lookAtPoint.position, duration, AxisConstraint.Y));








    //}

    //void PlayAnimation()
    //{
    //    // Trigger the animation
    //    animator.SetBool("isSitting", true);

    //    // Get the current animation state info
    //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    //    // Calculate the remaining time for the animation to finish
    //    float remainingTime = stateInfo.length * (1f - stateInfo.normalizedTime);

    //    // Create a sequence and add a delay for the remaining animation time
    //   /* sequence.AppendInterval(10f).Append(chair.transform.DOLookAt(lookAtPoint.position, duration, AxisConstraint.Y));*/ ;

    //    // Append the chair rotation after the delay

    //}

    void GetUp()
    {
        isSitting = false;


        // Start a DOTween sequence
        Sequence sequence = DOTween.Sequence();

        float distance = Vector3.Distance(player.transform.position, monitorCamera.position);

        // Calculate the duration based on the player's speed
        float durationMove = distance / 1.5f;

        sequence

            .Join(camera.DOFieldOfView(fieldOfView, duration))
            .Append(chair.transform.DOLookAt(rotateToSitPoint.position, duration, AxisConstraint.Y))
            .AppendCallback(() =>
            {
                playerController.GetComponent<CharacterController>().enabled = false;
                print(playerController.transform.position);
                print(camera.transform.position);
                float posY = playerController.transform.localPosition.y;
                playerController.transform.localPosition = new Vector3(camera.gameObject.transform.position.x, posY, camera.gameObject.transform.position.z);
                print(playerController.transform.position);
                print(camera.transform.position);
                playerController.GetComponent<CharacterController>().enabled = true;

                 player.transform.parent = playerController.transform;

            })
            //.Append(player.transform.DOLookAt(rotateToSitPoint.position, duration, AxisConstraint.Y))
            .Append(player.transform.DOLocalMove(new Vector3(0, 0, 0), 1))
             
             .JoinCallback(() =>
             {
                 print("aimatio");

                 PlayAnimation();

             })
             
             .AppendInterval(1f)
             .Join(player.transform.GetChild(0).DOLocalMove(new Vector3(0, -0.894f, 0), 1.5f))
             .Append(player.transform.DOLocalRotateQuaternion(Quaternion.identity, duration));
        playerController.enabled = true;
        // // Move player to monitor and rotate to sit point
        // sequence.Append(player.transform.DOMove(monitorCamera.position, durationMove))
        //     .Append(player.transform.DOLookAt(rotateToSitPoint.position, duration, AxisConstraint.Y))

        // // Trigger the animation and calculate remaining time
        // .AppendCallback(() =>
        // {
        //     PlayAnimation();
        // })
        // .AppendInterval(2f)
        //.Append(chair.transform.DOLookAt(lookAtPoint.position, duration, AxisConstraint.Y))
        //.Join(camera.transform.DOLookAt(lookAtPoint.position, duration))
        //.Join(camera.DOFieldOfView(monitorCamera.GetComponent<Camera>().fieldOfView, duration));

        ////animator.SetBool("isSitting", false);
        //// Step 1: Temporarily unparent camera to change its field of view independently
        ////camera.transform.SetParent(null, true);

        //// Step 2: Apply field of view change while the camera is detached
        //sequence.Append(camera.DOFieldOfView(fieldOfView, duration))

        //        // Step 3: Reparent the camera to the player so it moves along with the player
        //        //.AppendCallback(() => camera.transform.SetParent(player.transform, true))

        //        // Step 4: Move the player to the sit position and rotate to face the monitor
        //        .Append(player.transform.DOMove(oldPlayerPos, duration))


        //        // Step 5: Re-enable player control after the sequence completes
        //        .Append(player.transform.DOLocalRotateQuaternion(Quaternion.identity, duration)).onComplete = RestartPlayerPos;


        //camera.fieldOfView = fieldOfView;

        phone.ShowPhone();
    }


    void RestartPlayerPos()
    {
        //player.transform.localPosition = Vector3.zero;
        playerController.enabled = true;
        // Unlock the cursor for interaction
        Cursor.lockState = CursorLockMode.None;
    }
   
}
