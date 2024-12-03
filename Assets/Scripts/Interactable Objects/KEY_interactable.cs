using UnityEngine;

public class KEY_interactable : MonoBehaviour, IInteractable
{
    public int KeyId;

    public void Interact()
    {
        GlobalStateManager.AddKey(KeyId);
        Debug.Log($"Key {KeyId} collected. Current keys: {string.Join(", ", GlobalStateManager.CollectedKeys)}");
        Destroy(gameObject);
    }
}
