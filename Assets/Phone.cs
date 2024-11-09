using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField] Vector3 positionOffset;
    [SerializeField] Vector3 rotationOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward , Camera.main.transform.up );
        transform.position = Camera.main.ViewportToWorldPoint(positionOffset);
        transform.rotation *= Quaternion.Euler(rotationOffset);
    }
}
