using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOOR_Interacting : MonoBehaviour, IInteractable
{
    private bool isOpen = false;

    public void Interact()
    {
        if (!isOpen)
        {
            Debug.Log("You opened the door.");
            transform.Rotate(0f, 90f, 0f); 
            isOpen = true;
        }
        else
        {
            Debug.Log("You closed the door.");
            transform.Rotate(0f, -90f, 0f); 
            isOpen = false;
        }
    }

}
