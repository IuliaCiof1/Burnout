using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHONE_interactable : MonoBehaviour, IInteractable
{

    [SerializeField]public Phone phoneInHand;
    [SerializeField]public AudioClip PickUpSoundFX;
    private void Start()
    {

        //phoneInHand.HidePhone();
    }

    public void Interact()
    {
        Flashlight playerFlashlight = FindObjectOfType<Flashlight>();

        if (playerFlashlight != null)
            playerFlashlight.EnableFlashlight();

        phoneInHand.IsCollected = true;
        phoneInHand.ShowPhone();
        SoundFXManager.instance.PlaySoundFXClip(PickUpSoundFX, transform, 1f);
        //ObjectiveEvents.SendSpookyEmail();
        Destroy(gameObject);
        Debug.Log("You picked up a Phone");
    }
}
