using System.Collections;
using UnityEngine;

public class DOOR_Interacting : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    private bool isMoving = false;
    public bool isLocked = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private float rotationSpeed = 5f;
    [SerializeField] public AudioClip DoorRotateFX;
    [SerializeField] public AudioClip ClosedDoor;
    [SerializeField] private float rotationAngle = 90f;
    ActionManager action;
    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0f, rotationAngle, 0f) * closedRotation;
    }

    public void Interact()
    {
        if (!isMoving && !isLocked)
        {
            StartCoroutine(RotateDoor());
        }
        else if (isLocked) 
        {
            ActionManager.Instance.HandleTrigger(4, "It's Locked...", ClosedDoor);
        }
    }

    private IEnumerator RotateDoor()
    {

        SoundFXManager.instance.PlaySoundFXClip(DoorRotateFX, transform, 1f);
        isMoving = true;
        Quaternion targetRotation = isOpen ? closedRotation : openRotation;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;
        isOpen = !isOpen;
        isMoving = false;
    }
}
