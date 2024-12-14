using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections;

public class KEYPAD_interactable : MonoBehaviour, IInteractable
{
    #region - Declarations
    [SerializeField] private GameObject keypadUI;
    [SerializeField] private List<int> correctKeyCombo = new List<int> { 9, 1, 7, 0 };
    [SerializeField] private GameObject playerController;
    [SerializeField] private Transform keypadCameraTarget;
    [SerializeField] private float cameraMoveDuration = 1f;
    [SerializeField] private DOOR_Interacting door;
    [SerializeField] public AudioClip BeepSound;
    [SerializeField] public AudioClip Access_Denied;
    [SerializeField] public AudioClip Access_Granted;

    private List<int> currentInput = new List<int>();
    public static bool isInteracting = false;
    private Controller playerMovementScript;
    private InteractionHandler playerCorsshair;
    private Transform playerCamera;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    #endregion

    #region - Events
    private void Start()
    {
        if (keypadUI != null)
        {
            keypadUI.SetActive(false);
        }

        if (playerController != null)
        {
            playerMovementScript = playerController.GetComponent<Controller>();
            playerCorsshair = playerController.GetComponent<InteractionHandler>();
            playerCamera = playerMovementScript.playerCamera.transform;
        }
    }
    private void Update()
    {
        if (isInteracting && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitKeypadMode();
        }
    }
    #endregion

    #region - Methods
    public void Interact()
    {
        if (isInteracting) return;

        EnterKeypadMode();
    }

    private void EnterKeypadMode()
    {
        SetCrosshairVisibility(false);

        isInteracting = true;

        if (playerCamera != null)
        {
            originalCameraPosition = playerCamera.position;
            originalCameraRotation = playerCamera.rotation;

            playerCamera.DOMove(keypadCameraTarget.position, cameraMoveDuration);
            playerCamera.DORotateQuaternion(keypadCameraTarget.rotation, cameraMoveDuration);
        }

        if (keypadUI != null)
        {
            keypadUI.SetActive(true);
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.SetMovement(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Keypad interaction started.");
    }

    public void ExitKeypadMode()
    {
        SetCrosshairVisibility(true);

        isInteracting = false;

        if (playerCamera != null)
        {
            playerCamera.DOMove(originalCameraPosition, cameraMoveDuration);
            playerCamera.DORotateQuaternion(originalCameraRotation, cameraMoveDuration).OnComplete(()=> {
                if (playerMovementScript != null)
                {
                    playerMovementScript.SetMovement(true);
                }
            });
            
        }

        if (keypadUI != null)
        {
            keypadUI.SetActive(false);
        }

       

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ResetKeypad();
        Debug.Log("Keypad interaction ended.");
    }

    public void PressKey(int key)
    {
        if (!isInteracting) return;

        SoundFXManager.instance?.PlaySoundFXClip(BeepSound, transform, 0.8f);

        currentInput.Add(key);
        Debug.Log($"Key {key} pressed. Current input: {string.Join("", currentInput)}");

        if (currentInput.Count == correctKeyCombo.Count)
        {
            bool isCorrect = true;

            for (int i = 0; i < correctKeyCombo.Count; i++)
            {
                if (currentInput[i] != correctKeyCombo[i])
                {
                    isCorrect = false;
                    break;
                }
            }

            if (isCorrect)
            {
                SoundFXManager.instance?.PlaySoundFXClip(Access_Granted, transform, 0.8f);
                Debug.Log("Correct combination entered. Access granted.");
                UnlockDoor();
                ExitKeypadMode();
            }
            else
            {
                SoundFXManager.instance?.PlaySoundFXClip(Access_Denied, transform, 0.8f);
                Debug.LogWarning("Incorrect combination entered. Try again.");

                ResetKeypad();
            }
        }
    }

    private void ResetKeypad()
    {
        currentInput.Clear();
    }

    private void UnlockDoor()
    {
        if (door != null)
        {
            door.isLocked = false;
            door.Interact();
        }
        else
        {
            Debug.LogError("Door reference not set in KEYPAD_interactable.");
        }
    }

    private void SetCrosshairVisibility(bool state)
    {
        playerCorsshair.VisibleCorsshair(state);
    }
    #endregion
}
