using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChaseTrigger : MonoBehaviour
{
    public MonsterChaseAI monsterAI;
    [SerializeField] GameObject MonsterMesh;
    [SerializeField] GameObject Monster;
    [SerializeField] Transform SpawnLocation;
    [SerializeField] Collider MonsterCollider;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] public AudioClip NormalMusic;
    [SerializeField] GameObject WallToBeDistroied;
    public bool endChase = true;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (endChase)
            {
                MonsterMesh.SetActive(false);
                MonsterCollider.enabled = false;
            }
            else
            {
                MonsterMesh.SetActive(false);
                Monster.transform.position = SpawnLocation.position;
                Monster.transform.rotation = SpawnLocation.rotation; // Match rotation if needed
                MonsterMesh.SetActive(true);
            }


            if (AudioSource != null)
            {
                AudioSource.clip = NormalMusic;
                AudioSource.Play();
                AudioSource.loop = true;
            }

            monsterAI.StopChase();
 
        }
    }
}
