using System.Collections;
using UnityEngine;

public class TELP_DOOR_interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private float teleportDelay = 0.5f;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform spawnPoint;

    private bool isInteracting = false; 

    public void Interact()
    {
        if (isInteracting)
        {
            Debug.LogWarning("Door is already being interacted with.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("SpawnPoint is not set! Please assign a valid spawn point.");
            return;
        }

        if (playerTransform == null)
        {
            Debug.LogError("PlayerTransform is not set! Please assign a valid player transform.");
            return;
        }

        StartCoroutine(OpenAndTeleport());
    }

    private IEnumerator OpenAndTeleport()
    {
        isInteracting = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }

        if (doorSound != null)
        {
            SoundFXManager.instance?.PlaySoundFXClip(doorSound, transform, 1f);
        }

        yield return new WaitForSeconds(teleportDelay);

        TeleportPlayer();

        isInteracting = false;
    }

    private void TeleportPlayer()
    {
        playerTransform.position = spawnPoint.position;
        playerTransform.rotation = spawnPoint.rotation;
        Debug.Log($"Player teleported to {spawnPoint.name}.");
    }
}
