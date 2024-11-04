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


    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            //ignore mouse clicks
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                return;
            }
            else
            {
                if (index < textToWrite.Length - 1)
                    textArea.text += textToWrite[index];
                index++;
            }
        }
    }

}
