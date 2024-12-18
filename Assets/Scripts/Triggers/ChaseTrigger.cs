using Unity.VisualScripting;
using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{
    public MonsterChaseAI monsterAI;
    [SerializeField] GameObject MonsterMesh;
    [SerializeField] Collider MonsterCollider;
    [SerializeField] AudioSource AudioSource;
    [SerializeField] public AudioClip ChaseMusic;
    [SerializeField] public AudioClip Scream;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MonsterMesh.SetActive(true);
            MonsterCollider.enabled = true;
            AudioSource.clip = ChaseMusic;  // Set the chase music
            AudioSource.Play();  // Play chase music
            AudioSource.loop = true;  // Make sure chase music loops
            monsterAI.StartChase();
            SoundFXManager.instance.PlaySoundFXClip(Scream, transform, 1f);
        }
    }
}
