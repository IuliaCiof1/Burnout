using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public static InteractionHandler instance;

    public Transform InteractorSource;
    public float InteractorRange = 4f;

    public GameObject Cross;
    public GameObject Eye;
    public GameObject Hand;

    private GameObject currentTarget;

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

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (currentTarget != null && currentTarget.TryGetComponent(out IInspectable inspecObj))
            {
                inspecObj.Inspect();
            }
        }
    }

    private void HandleHighlighting()
    {
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractorRange))
        {
            GameObject hitObject = hitInfo.collider.gameObject;

            if (hitObject != currentTarget)
            {
                currentTarget = hitObject;

                if (hitObject.TryGetComponent(out IInspectable _))
                {
                    SetCrosshair(Eye);
                }
                else if (hitObject.TryGetComponent(out IInteractable _))
                {
                    SetCrosshair(Hand);
                }
                else
                {
                    SetCrosshair(Cross);
                }
            }
        }
        else
        {
            currentTarget = null;
            SetCrosshair(Cross);
        }
    }

    private void SetCrosshair(GameObject activeCrosshair)
    {
        Cross.SetActive(false);
        Eye.SetActive(false);
        Hand.SetActive(false);

        activeCrosshair.SetActive(true);
    }

    public void VisibleCorsshair(bool state)
    {
        Cross.SetActive(state);
        Eye.SetActive(state);
        Hand.SetActive(state);

        if (state)
        {
            SetCrosshair(Cross);
        }
    }

}
