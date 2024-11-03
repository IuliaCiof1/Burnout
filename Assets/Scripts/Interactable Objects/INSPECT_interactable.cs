using UnityEngine;

public class INSPECT_interactable : MonoBehaviour, IInspectable
{
    private bool isBeingInspected = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Camera mainCamera;
    public float rotationSpeed = 100f;

    private Controller playerController;
    private Collider objectCollider;

    private void Start()
    {
        mainCamera = Camera.main;
        playerController = FindObjectOfType<Controller>();
        objectCollider = GetComponent<Collider>();
    }

    public void Inspect()
    {
        if (!isBeingInspected)
        {
            StartInspection();
        }
        else
        {
            StopInspection();
        }
    }

    private void StartInspection()
    {
        isBeingInspected = true;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        if (objectCollider != null)
        {
            objectCollider.enabled = false;
        }

        float objectSize = 1f;
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            objectSize = renderer.bounds.extents.magnitude;
        }
        else if (objectCollider != null)
        {
            objectSize = objectCollider.bounds.extents.magnitude;
        }

        float inspectionDistance = Mathf.Max(objectSize * 3.00f, 0.5f);

        Vector3 inspectionPosition = mainCamera.transform.position + mainCamera.transform.forward * inspectionDistance;

        transform.position = inspectionPosition;

        transform.LookAt(mainCamera.transform);

        playerController.SetMovement(false);
    }

    private void StopInspection()
    {
        isBeingInspected = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;


        if (objectCollider != null)
        {
            objectCollider.enabled = true;
        }

        playerController.SetMovement(true);
    }

    private void Update()
    {
        if (isBeingInspected)
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(mainCamera.transform.up, -rotationX, Space.World);
            transform.Rotate(mainCamera.transform.right, rotationY, Space.World);

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                StopInspection();
            }
        }
    }
}