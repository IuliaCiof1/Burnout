using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KEYPAD_interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject keypadUI;
    [SerializeField] private List<int> correctKeyCombo = new List<int> { 1, 2, 3, 4 };
    [SerializeField] private GameObject playerController;

    private List<int> currentInput = new List<int>();
    private bool isInteracting = false;
    private Controller playerMovementScript;

    private void Start()
    {
        if (keypadUI != null)
        {
            keypadUI.SetActive(false);
        }

        if (playerController != null)
        {
            playerMovementScript = playerController.GetComponent<Controller>();
        }
    }

    public void Interact()
    {
        if (isInteracting) return;

        EnterKeypadMode();
    }

    private void EnterKeypadMode()
    {
        isInteracting = true;

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
        isInteracting = false;

        if (keypadUI != null)
        {
            keypadUI.SetActive(false);
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.SetMovement(true);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Keypad interaction ended.");
    }

    public void PressKey(int key)
    {
        if (!isInteracting) return;

        currentInput.Add(key);
        Debug.Log($"Key {key} pressed. Current input: {string.Join("", currentInput)}");

        if (currentInput.Count <= correctKeyCombo.Count)
        {
            for (int i = 0; i < currentInput.Count; i++)
            {
                if (currentInput[i] != correctKeyCombo[i])
                {
                    Debug.LogWarning("Incorrect input. Resetting keypad.");
                    ResetKeypad();
                    return;
                }
            }

            if (currentInput.Count == correctKeyCombo.Count)
            {
                Debug.Log("Correct combination entered. Access granted.");
                ExitKeypadMode();
            }
        }
    }

    private void ResetKeypad()
    {
        currentInput.Clear();
    }
}
