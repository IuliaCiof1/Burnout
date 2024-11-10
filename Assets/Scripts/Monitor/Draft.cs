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
            //ignore mouse clicks
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                return;
            }
            else
            {
                if (index < textToWrite.Length - 1)
                {
                    textArea.text += textToWrite[index];
                    index++;
                }
            }
        }
    }


    public void SendDraft()
    {
        
        if (index == textToWrite.Length - 1)
        {
            ObjectiveEvents.SendEmail();
            draftMail.SetActive(false);

            StartCoroutine(Delay());
        }
        else
            print("I need to finish writing the mail first");
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(spookyMailDelay);
        spookyMail.SetActive(true);
    }
}
