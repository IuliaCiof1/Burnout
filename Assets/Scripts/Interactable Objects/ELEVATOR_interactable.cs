using System.Collections;
using UnityEngine;

public class ELEVATOR_interactable : MonoBehaviour, IInteractable
{
    public Transform targetPosition;
    public float elevatorSpeed = 1.0f;
    public Animator doorAnimator;
    private bool isMoving = false;
    private bool playerInside = false;
    private GameObject player;
    public Collider restrictingCollider;

    [SerializeField] Objective objectiveToComplete;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        print(player.name);
        restrictingCollider.enabled = false;
    }

    public void Interact()
    {
        if (!isMoving && objectiveToComplete.isCompleted)
        {
            StartCoroutine(OpenDoors());
        }
    }

    private IEnumerator OpenDoors()
    {
        doorAnimator.SetTrigger("openDoor");
        yield return new WaitForSeconds(2f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObjectiveEvents.TakeLift();

            playerInside = true;
            restrictingCollider.enabled = true;
            StartCoroutine(CloseDoorsAndMove());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorAnimator.SetTrigger("closeDoor");
            restrictingCollider.enabled = false;
            playerInside = false;
        }
    }

    private IEnumerator CloseDoorsAndMove()
    {

        if (playerInside)
        {
            GameObject playerManager = GameObject.FindWithTag("PlayerManager");
            //if (playerManager != null)
            //{
            //    playerManager.transform.SetParent(transform);
            //}

            doorAnimator.SetTrigger("closeDoor");
            yield return new WaitForSeconds(4f);

            StartCoroutine(MoveToTarget());
        }
    }

    private IEnumerator MoveToTarget()
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, elevatorSpeed * Time.deltaTime);
            yield return null;
        }
        restrictingCollider.enabled = false;
        doorAnimator.SetTrigger("openDoor");
        isMoving = false;
    }
}
