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

    public void HandleTrigger(int triggerID, string description, AudioClip Sound)
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
            case 4:
                break;
            case 5:
                PlayDoorAudio();
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
        Debug.Log("Showing scary  ...");
    }

    private void PlaySoundEffect()
    {
        Debug.Log("Playing sound effect...");
    }

    private void PlayDoorAudio()
    {
        Debug.Log("ABC");
    }
}
