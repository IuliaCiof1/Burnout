using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CH1_Cockroach : MonoBehaviour
{
    private State currentState;
    public Vector3 direction;
    private float speed = 0.4f;

    public Animator CS1_Anim;


    private float changeDirectionTimer;
    private float randomChangeTime;
    public int flockID;  // ID for the specific flock/group

    public float separationDistance = 10.0f;
    public float alignmentDistance = 10.0f;
    public float cohesionDistance = 10.0f;
    public float maxFlockSpeed = 0.4f;

    private float initialYPosition;


    private static List<CH1_Cockroach> allCockroaches = new List<CH1_Cockroach>();

    void Start()
    {
        allCockroaches.Add(this);
        initialYPosition = transform.position.y;
        ChangeState(new CH1_Moving());
    }

    void Update()
    {
        if (currentState is CH1_Dead) return;

        currentState?.Update(this);
        changeDirectionTimer -= Time.deltaTime;

        if (changeDirectionTimer <= 0)
        {
            SetRandomDirection();
        }

        RotateTowardsDirection();
        MaintainYPosition();
    }


    void OnDestroy()
    {
        allCockroaches.Remove(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeState(new CH1_Dead());
        }
    }

    void MaintainYPosition()
    {
        Vector3 position = transform.position;
        position.y = initialYPosition;
        transform.position = position;
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
        GameObject Player = GameObject.FindWithTag("Player");
        bool isNearby = Vector3.Distance(transform.position, Player.transform.position) < 3f;
        return isNearby;
    }

    public void SetRandomDirection()
    {
        direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
        randomChangeTime = UnityEngine.Random.Range(1f, 3f);
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

    public void ApplyFlockingBehavior()
    {
        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;

        int separationCount = 0;
        int alignmentCount = 0;
        int cohesionCount = 0;

        foreach (var cockroach in allCockroaches)
        {
            if (cockroach == this || cockroach.flockID != this.flockID) continue;

            float distance = Vector3.Distance(transform.position, cockroach.transform.position);

            if (distance < separationDistance)
            {
                separation += (transform.position - cockroach.transform.position).normalized / distance;
                separationCount++;
            }

            if (distance < alignmentDistance)
            {
                alignment += cockroach.direction;
                alignmentCount++;
            }

            if (distance < cohesionDistance)
            {
                cohesion += cockroach.transform.position;
                cohesionCount++;
            }
        }

        if (separationCount > 0) separation /= separationCount;
        if (alignmentCount > 0) alignment /= alignmentCount;
        if (cohesionCount > 0) cohesion = (cohesion / cohesionCount - transform.position).normalized;

        Vector3 flockDirection = separation + alignment + cohesion;
        if (flockDirection != Vector3.zero)
        {
            direction = Vector3.Lerp(direction, flockDirection.normalized, 0.1f);
            speed = Mathf.Lerp(speed, maxFlockSpeed, 0.05f);
        }
    }

    public void SetResting(bool isResting)
    {
        if (isResting)
        {
            speed = 0f;
            //idle animation!!!
        }
        else
        {
            speed = 0.4f;
        }
    }

    public void DisableMovement()
    {
        speed = 0f;
    }

    public void RotateTowardsDirection()
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-direction, Vector3.up);
            targetRotation.eulerAngles = new Vector3(0, targetRotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    #region - Animations
    public void SetDeadVisual()
    {
        CS1_Anim.SetBool("anim_death", true);
    }

    public void SetFleeingVisual()
    {
        CS1_Anim.SetTrigger("anim_Fleeing");
    }

    public void SetMovingVisual() 
    {
        CS1_Anim.SetTrigger("anim_Moving");
    }

    public void SetIdleVisual()
    {
        CS1_Anim.SetTrigger("anim_Idle");
    }
    #endregion
}