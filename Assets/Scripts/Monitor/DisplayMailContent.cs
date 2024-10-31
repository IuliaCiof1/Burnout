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
            Button mailButton =  mail.GetComponent<Button>();

            mailButton.onClick.AddListener(() => { DisplayMail(mail.gameObject); });
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
}
