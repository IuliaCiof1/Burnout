using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChaseTrigger : MonoBehaviour
{
    public MonsterChaseAI monsterAI;
    [SerializeField] GameObject MonsterMesh;
    [SerializeField] Collider MonsterCollider;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] public AudioClip NormalMusic;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MonsterMesh.SetActive(false);
            MonsterCollider.enabled = false;
            AudioSource.clip = NormalMusic;  // Set the chase music
            AudioSource.Play();  // Play chase music
            AudioSource.loop = true;  // Make sure chase music loops
            monsterAI.StartChase();
        }
    }
}
