using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHONE_interactable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject);
        Debug.Log("You picked up a Phone");
    }
}
