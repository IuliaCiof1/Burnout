using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLASH_interactable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Flashlight playerFlashlight = FindObjectOfType<Flashlight>();

        if (playerFlashlight != null)
            playerFlashlight.EnableFlashlight();

        Destroy(gameObject);
        Debug.Log("You picked up a Flashlight");
    }
}
