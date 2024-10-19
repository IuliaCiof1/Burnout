using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractorRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractorRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}