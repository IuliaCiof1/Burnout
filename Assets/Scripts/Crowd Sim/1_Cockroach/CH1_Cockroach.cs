using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH1_Cockroach : MonoBehaviour
{
    private State currentState;
    private Vector3 direction;
    private float speed = 2.0f;

    private float changeDirectionTimer;
    private float randomChangeTime;

    void Start()
    {
        ChangeState(new CH1_Moving());
    }

    void Update()
    {
        currentState?.Update(this);
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            SetRandomDirection();
        }
    }

    public void ChangeState(State newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public bool IsPlayerNearby()
    {
        GameObject player = GameObject.FindWithTag("Player");
        return player != null && Vector3.Distance(transform.position, player.transform.position) < 1f;
    }

    public void SetRandomDirection()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        randomChangeTime = Random.Range(1f, 3f);
        changeDirectionTimer = randomChangeTime;
    }

    public void SetFleeDirection()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            direction = (transform.position - player.transform.position).normalized;
        }
    }
}
