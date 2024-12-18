using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_LoadState : MonoBehaviour
{
    [SerializeField] private GameObject CSManager;
    [SerializeField] public bool CHStatus;

    private GameObject[] CHs;

    void Start()
    {
        int childCount = CSManager.transform.childCount;
        CHs = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            CHs[i] = CSManager.transform.GetChild(i).gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnloadAllCHs(CHStatus);
        }
    }

    private void UnloadAllCHs(bool CHStatus)
    {
        foreach (GameObject ch in CHs)
        {
            ch.SetActive(CHStatus);
        }
    }
}
