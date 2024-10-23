using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractorRange = 5f;

    private GameObject currentTarget;
    private Material originalMaterial;

    public Material outlineMaterial;

    private void Update()
    {
        HandleHighlighting();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTarget != null && currentTarget.TryGetComponent(out IInteractable interactObj))
            {
                interactObj.Interact();
            }
        }
    }

    private void HandleHighlighting()
    {
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractorRange))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (hitObject.TryGetComponent(out IInteractable interactable))
            {
                if (hitObject != currentTarget)
                {
                    ClearOutline();

                    currentTarget = hitObject;
                    Renderer renderer = currentTarget.GetComponent<Renderer>();

                    if (renderer != null)
                    {
                        originalMaterial = renderer.material;
                        renderer.material = outlineMaterial;
                    }
                }
            }
            else
            {
                ClearOutline();
            }
        }
        else
        {
            ClearOutline();
        }
    }

    private void ClearOutline()
    {
        if (currentTarget != null)
        {
            Renderer renderer = currentTarget.GetComponent<Renderer>();
            if (renderer != null && originalMaterial != null)
            {
                renderer.material = originalMaterial;
            }

            currentTarget = null;
        }
    }
}
