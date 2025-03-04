using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMailContent : MonoBehaviour
{
    [SerializeField] private Transform contentMailPosition;
    [SerializeField] private DOOR_Interacting doorToUnlock;
    Vector3 iniPosition;

    private void Start()
    {
        foreach(Transform mail in transform)
        {
            Button mailButton =  mail.Find("Sender").GetComponent<Button>();
           


            mailButton.onClick.AddListener(() => { DisplayMail(mail.gameObject); });

            Transform unreadIcon = mail.Find("UnreadIcon");
            if (unreadIcon != null)
            {
               
                mailButton.onClick.AddListener(() => { ReadMail(mail.gameObject, unreadIcon.gameObject); });

            }
           
        }
    }

    //sets the MailContent object of all the other unselected mails, on not active. Also corectly repositions the mailContent
    public void DisplayMail(GameObject mail)
    {
        
        foreach (Transform inactiveMail in transform)
        {
            inactiveMail.transform.GetChild(1).gameObject.SetActive(false);
        }

        GameObject mailContent = mail.transform.GetChild(1).gameObject;
        mailContent.SetActive(true);
        mailContent.transform.position = contentMailPosition.transform.position;

  
    }


    public void ReadMail(GameObject mail, GameObject  unreadIcon)
    {
        unreadIcon.SetActive(false);
        print(mail.name);
        if (mail.CompareTag("SpookyMail"))
        {
            print("aaaaa de ce");
            ObjectiveEvents.OpenSpookyMail(doorToUnlock);
        }
    }

}
