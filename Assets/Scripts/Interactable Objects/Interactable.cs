using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactable : MonoBehaviour, IInteractable
{
    public virtual void Interact()
    {
    }
}
