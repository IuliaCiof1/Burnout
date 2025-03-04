using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEditor.Rendering;
using Unity.VisualScripting;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance { get; private set; }

    [SerializeField] TMP_Text dialogTextUI;
    GameObject textContainer;

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

        textContainer = dialogTextUI.transform.parent.gameObject;
    }



    public void HandleTrigger(int triggerID, string dialogText, AudioClip Sound = null, List<GameObject> triggerObjects = null)
    {

        switch (triggerID)
        {
            case 1:
                StartDialogue(dialogText);
                if (Sound != null)
                    SoundFXManager.instance.PlaySoundFXClip(Sound, transform, 1f);
                break;
            case 2:
                ShowScaryObject(triggerObjects);
                break;
            case 3:
                PlaySoundEffect(Sound);
                break;
            case 4:
                DoorIsClosed(dialogText, Sound);
                break;
            case 5:
                PlayDoorAudio();
                break;
            case 6:
                HideScaryObject(triggerObjects);
                break;
            default:
                Debug.LogWarning("Unhandled triggerID: " + triggerID);
                break;
        }
    }

    private void StartDialogue(string dialogText)
    {
        textContainer.SetActive(true);
        dialogTextUI.text = dialogText;
        StartCoroutine(DialogTimer());
        Debug.Log("[]: " + dialogText);
    }

    IEnumerator DialogTimer()
    {
        yield return new WaitForSeconds(4);
        textContainer.SetActive(false);
    }

    private void ShowScaryObject(List<GameObject> triggerObjects)
    {
        print("show");
        foreach (GameObject triggerObject in triggerObjects)
        {

            triggerObject.SetActive(true);
        }

    }

    private void HideScaryObject(List<GameObject> triggerObjects)
    {
        print("hide");
        foreach (GameObject triggerObject in triggerObjects)
            triggerObject.SetActive(false);

    }

    private void PlaySoundEffect(AudioClip sound)
    {
        SoundFXManager.instance.PlaySoundFXClip(sound, transform, 1f);
    }

    private void PlayDoorAudio()
    {
        Debug.Log("ABC");
    }

    private void DoorIsClosed(string dialog, AudioClip sound)
    {
        StartDialogue(dialog);

        if (sound != null)
        {
            SoundFXManager.instance.PlaySoundFXClip(sound, transform, 1f);
        }
    }

}
