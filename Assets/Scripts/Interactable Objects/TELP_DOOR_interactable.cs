using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TELP_DOOR_interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private Transform playerManager;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] public List<int> RequiredKeys = new List<int>();

    public bool isLocked = false;
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
        bool hasAllKeys = RequiredKeys.TrueForAll(key => GlobalStateManager.HasKey(key));

        if (hasAllKeys)
        {
            OpenAndTeleport();
        }
        else
        {
            ActionManager.Instance.HandleTrigger(4, "The Door knob is missing. I need to find one... maybe in the maintenance room?", null);
        }
    }

    private void OpenAndTeleport()
    {
        if (!isLocked)
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
    }

    private void TeleportPlayer()
    {
         Collider playerCollider = playerManager.GetComponentInChildren<Collider>();
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
            Debug.Log("Player collider disabled.");
        }
        else
        {
            Debug.LogWarning("No Collider found on the player.");
        }

        playerManager.position = spawnPoint.position;
        playerManager.rotation = spawnPoint.rotation;

        Debug.Log($"Player teleported to {spawnPoint.position}.");

        if (playerCollider != null)
        {
            playerCollider.enabled = true;
            Debug.Log("Player collider re-enabled.");
        }
    }
}
