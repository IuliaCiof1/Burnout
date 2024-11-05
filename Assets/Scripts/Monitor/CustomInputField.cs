using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CustomInputField : MonoBehaviour, IDeselectHandler, ISelectHandler
{
    private bool fieldFocused;

    [SerializeField] TMP_Text inputText;
    [SerializeField] TMP_Text placeHolder;
    [SerializeField] int maxCharacters;

    void Awake()
    {
        placeHolder.enabled = true;
    }

    public void OnSelect(BaseEventData eventData)
    {
        fieldFocused = true;
        placeHolder.enabled = false;
        Debug.Log("Field focused");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        //used fo AWSD keys to not deselect the field if those keys are pressed
        if (Input.inputString.Length>0 && EventSystem.current
          && EventSystem.current.currentSelectedGameObject
          )
            return;

        fieldFocused = false;
        if (inputText.text.Length == 0)
        {
            placeHolder.enabled = true;
        }
        Debug.Log("Field deselected");
    }

    private void Update()
    {
        if (fieldFocused && EventSystem.current.currentSelectedGameObject != gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        if (fieldFocused)
        {
            foreach (char c in Input.inputString)
            {
                if (c == '\b') // Handle backspace
                {
                    if (inputText.text.Length > 0)
                    {
                        inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
                    }
                }
                else if (c == '\n' || c == '\r') // Handle Enter key
                {
                    Debug.Log("User pressed Enter: " + inputText.text);
                    fieldFocused = false;
                }
                else if(inputText.text.Length<maxCharacters) // Add typed character
                {
                    inputText.text += c;
                }
            }
        }
    }
}