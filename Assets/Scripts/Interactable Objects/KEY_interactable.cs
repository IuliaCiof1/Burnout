using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KEY_interactable : MonoBehaviour, IInteractable
{
    public int KeyId;
    public void Interact()
    {
        Debug.Log("You picked a key");
        Destroy(gameObject);
    }
}
