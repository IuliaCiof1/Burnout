using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField] Vector3 positionOffset;
    Vector3 hidePosition;
    Vector3 showPosition;


    [SerializeField] Vector3 rotationOffset;
    public bool IsCollected{ get; set; }

    private bool isHidden;

    // Start is called before the first frame update
    void Start()
    {
        //hidePosition = transform.position - new Vector3(0, 3, 0);
        

        HidePhone();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward , Camera.main.transform.up );
        transform.position = Camera.main.ViewportToWorldPoint(positionOffset);
        transform.rotation *= Quaternion.Euler(rotationOffset);
    }

    public void HidePhone()
    {
        if (!isHidden)
        {
            isHidden = true;
            //showPosition = transform.localPosition;
            positionOffset -= new Vector3(0, 3, 0);
        }
    }

    public void ShowPhone()
    {
        print(IsCollected);
        if (IsCollected && isHidden)
        {
            //transform.localPosition = showPosition;
            isHidden = false;
            positionOffset += new Vector3(0, 3, 0);
            
        }
    }
}
