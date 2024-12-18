using UnityEngine;
using UnityEngine.AI;

public class MonsterChaseAI : MonoBehaviour
{
    public Transform player;
    public float closeRange = 2f;
    public float farRange = 10f;
    public float baseSpeed = 2.5f;
    public float maxSpeed = 5.5f;
    public float minSpeed = 1f;
    public float jumpscareRange = 3f;
    public bool isChasing = false;
    public NavMeshAgent agent;
    public Controller playerController;

    private bool playerCaught = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
        agent.isStopped = true;
    }

    void Update()
    {
        if (!isChasing || playerCaught) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > farRange)
        {
            agent.speed = maxSpeed;
        }
        else if (distanceToPlayer < closeRange)
        {
            agent.speed = minSpeed;
        }
        else
        {
            agent.speed = baseSpeed;
        }

        agent.SetDestination(player.position);

        if (distanceToPlayer <= jumpscareRange)
        {
            TriggerJumpscare();
        }
    }

    void TriggerJumpscare()
    {
        if (playerCaught) return;
        playerCaught = true;

        agent.isStopped = true;
        Debug.Log("Jumpscare activated!");

        playerController.canMove = false;

        Debug.Log("Player has been caught. Game Over!");
        Invoke(nameof(EndGame), 2f);
    }

    void EndGame()
    {
        Debug.Log("Restarting game...");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void StartChase()
    {
        isChasing = true;
        gameObject.SetActive(true);
        agent.isStopped = false;
    }

    public void StopChase()
    {
        isChasing = false;
        gameObject.SetActive(false);
        agent.isStopped = true;
    }
}
