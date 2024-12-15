using UnityEngine;

public class RightLeftCorridorTrigger : MonoBehaviour
{
    public GameObject wall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (wall != null)
            {
                wall.SetActive(true);

                Collider wallCollider = wall.GetComponent<Collider>();
                if (wallCollider != null)
                {
                    wallCollider.enabled = true; 
                }
                else
                {
                    Debug.LogWarning("No collider found on the wall GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("Wall GameObject is not assigned.");
            }
        }
    }
}
