using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandleTrigger(int triggerID)
    {

        switch (triggerID)
        {
            case 1:
                StartDialogue();
                break;
            case 2:
                ShowScaryObject();
                break;
            case 3:
                PlaySoundEffect();
                break;
            default:
                Debug.LogWarning("Unhandled triggerID: " + triggerID);
                break;
        }
    }

    private void StartDialogue()
    {
        Debug.Log("Starting Dialogue...");
    }

    private void ShowScaryObject()
    {
        Debug.Log("Showing scary object...");
    }

    private void PlaySoundEffect()
    {
        Debug.Log("Playing sound effect...");
    }
}
