using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHONE_interactable : MonoBehaviour, IInteractable
{

    [SerializeField]public Phone phoneInHand;

    private void Start()
    {
        phoneInHand.HidePhone();
    }

    public void Interact()
    {
        Flashlight playerFlashlight = FindObjectOfType<Flashlight>();

        if (playerFlashlight != null)
            playerFlashlight.EnableFlashlight();

        phoneInHand.ShowPhone();
        ObjectiveEvents.SendSpookyEmail();
        Destroy(gameObject);
        Debug.Log("You picked up a Phone");


    }
}
