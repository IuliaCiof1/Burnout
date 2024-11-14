using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialog : MonoBehaviour
{
    [SerializeField] [TextArea] string dialogText;

    // Start is called before the first frame update
    void Start()
    {
        ActionManager.Instance.HandleTrigger(1, dialogText, null, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
