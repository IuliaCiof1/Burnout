using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Draft : MonoBehaviour
{
    int index;

    [TextArea(15, 20)]
    [SerializeField] private string textToWrite;
    [SerializeField] TMP_Text textArea;
    [SerializeField] private GameObject draftMail;

    [SerializeField] GameObject spookyMail;
    [SerializeField] float spookyMailDelay;

    [SerializeField] [TextArea] string dialogText;
    bool dialogFinished;
    public AudioClip[] KeyStroke;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (textArea.gameObject.activeInHierarchy && Input.anyKeyDown)
        {
            
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                return;
            }
            else
            {
                if (index < textToWrite.Length - 1)
                {
                    SoundFXManager.instance.PlaySoundFXClips(KeyStroke, transform, 1f);

                    textArea.text += textToWrite[index];
                    index++;

                    if (!dialogFinished)
                    {
                        ActionManager.Instance.HandleTrigger(1, dialogText, null, null);
                        dialogFinished = true;
                    }
                }
                
            }
        }
    }


    public void SendDraft()
    {

        if (index == textToWrite.Length - 1)
        {
            ObjectiveEvents.SendEmail();

            ObjectiveEvents.OnSpookyEmailSent += SendSpookyMail;
            //draftMail.SetActive(false);
            print("aaaa1");
            //ObjectiveEvents.OnOpenSpookyMail += SendSpookyMail;

            //StartCoroutine(Delay());
        }
        else
        {
            dialogText = "I need to finish writing the mail first";
            ActionManager.Instance.HandleTrigger(1, dialogText, null, null);
            //print("I need to finish writing the mail first");
        }
    }


    public void SendSpookyMail()
    {
        print("aaaaaa");
        StartCoroutine(Delay());
        draftMail.SetActive(false);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(spookyMailDelay);
        spookyMail.SetActive(true);
        ObjectiveEvents.OnSpookyEmailSent -= SendSpookyMail;

    }
}
