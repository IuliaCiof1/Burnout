using System.Collections;
using UnityEngine;

public class ELEVATOR_interactable : MonoBehaviour, IInteractable
{
    public Transform targetPosition;
    public float elevatorSpeed = 1.0f;
    public Animator doorAnimator;
    private bool isMoving = false;
    private GameObject player;
    private bool isPlyerInside = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlyerInside = true;
    }


    public void Interact()
    {
        if (!isMoving)
        {
            StartCoroutine(OpenDoorsThenMove());
        }
    }

    private IEnumerator OpenDoorsThenMove()
    {
        if (!isPlyerInside)
        isMoving = true;
        //check for player leaving
        doorAnimator.SetBool("hasStarted", true);
        yield return new WaitForSeconds(6f);
        yield return StartCoroutine(MoveToTarget());
        yield return new WaitForSeconds(2f);
        doorAnimator.SetBool("hasArrived", true);
        //check for player leaving
        doorAnimator.SetBool("closeDoor", true);

        isMoving = false;
    }

    private IEnumerator MoveToTarget()
    {
        while (Vector3.Distance(transform.position, targetPosition.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, elevatorSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
