using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMailContent : MonoBehaviour
{
    [SerializeField] private Transform currentMail;

    Vector3 iniPosition;

    private void Start()
    {
        foreach(Transform mail in transform)
        {
            Button mailButton =  mail.Find("Sender").GetComponent<Button>();
           


            mailButton.onClick.AddListener(() => { DisplayMail(mail.gameObject); });

            GameObject unreadIcon = mail.Find("UnreadIcon").gameObject;
            if (unreadIcon != null);
                mailButton.onClick.AddListener(() => {ReadMail(mail.gameObject, unreadIcon); });
        }
    }

    public void DisplayMail(GameObject mail)
    {
        print(mail.name);
        foreach (Transform inactiveMail in transform)
        {
            inactiveMail.transform.GetChild(1).gameObject.SetActive(false);
        }

        mail.transform.GetChild(1).gameObject.SetActive(true);

    }


    public void ReadMail(GameObject mail, GameObject unreadIcon)
    {
        unreadIcon.SetActive(false);
    }

}
