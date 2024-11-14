using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    Quaternion rotation;


    private void Start()
    {
        rotation = transform.rotation;
        print(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTransform);

        transform.rotation = Quaternion.Euler(new Vector3(rotation.eulerAngles.x, transform.rotation.eulerAngles.y, rotation.eulerAngles.z));
    }
}
