using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVFLASH_interactable : MonoBehaviour, IInteractable
{

    [SerializeField]public UVFlash uvFlash;
    [SerializeField]public AudioClip PickUpSoundFX;
    [SerializeField] private DOOR_Interacting doorToUnlock;

    private void Start()
    {

        //phoneInHand.HidePhone();
    }

    public void Interact()
    {
        doorToUnlock.isLocked = false;
        SoundFXManager.instance.PlaySoundFXClip(PickUpSoundFX, transform, 1f);
        //ObjectiveEvents.SendSpookyEmail();
        Destroy(gameObject);
        Debug.Log("You picked up a UVFLash");
        GlobalStateManager.AddKey(0);
 
    }
}
