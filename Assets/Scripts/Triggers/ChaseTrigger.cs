using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{
    public MonsterChaseAI monsterAI;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            monsterAI.StartChase();
        }
    }
}
