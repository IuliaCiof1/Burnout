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
        // Find the Collider component on the player (child of playerManager)
        Collider playerCollider = playerManager.GetComponentInChildren<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false; // Turn off the collider
            Debug.Log("Player collider disabled.");
        }
        else
        {
            Debug.LogWarning("No Collider found on the player.");
        }

        // Teleport the player
        playerManager.position = spawnPoint.position;
        playerManager.rotation = spawnPoint.rotation;

        Debug.Log($"Player teleported to {spawnPoint.position}.");

        // Reactivate the collider after teleportation (optional)
        if (playerCollider != null)
        {
            playerCollider.enabled = true; // Turn the collider back on
            Debug.Log("Player collider re-enabled.");
        }
    }
}
