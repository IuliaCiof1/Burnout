using UnityEngine;

public class TELP_DOOR_interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private Transform playerManager; 
    [SerializeField] private Transform spawnPoint; 

    private bool isInteracting = false;

    public void Interact()
    {
        if (isInteracting)
        {
            Debug.LogWarning("Door is already being interacted with.");
            return;
        }

        if (spawnPoint == null || playerManager == null)
        {
            Debug.LogError("SpawnPoint or PlayerManager is not set! Please assign valid references.");
            return;
        }

        OpenAndTeleport();
    }

    private void OpenAndTeleport()
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

        TeleportPlayer();

        isInteracting = false;
    }

    private void TeleportPlayer()
    {
        playerManager.position = spawnPoint.position;
 
        Debug.Log($"Player teleported to {spawnPoint.position}.");
    }
}
