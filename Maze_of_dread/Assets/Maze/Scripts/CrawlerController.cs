using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  

public class CrawlerController : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public float attackRadius = 0.5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3f;

    private Animator animator;    
    private NavMeshAgent agent;
    private Vector3 patrolPoint;
    private bool isPatrolling = true;
    private bool isChasing = false;
    //private bool isWalking = true;
    private bool isAttacking = false;
    private Vector3 roomMinBounds;
    private Vector3 roomMaxBounds;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        Collider roomCollider = transform.parent.GetComponent<Collider>();
        roomMinBounds = roomCollider.bounds.min;
        roomMaxBounds = roomCollider.bounds.max;

        SetNewPatrolPoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRadius)
        {
            StartChasingPlayer();
        }
        else if (isChasing)
        {
            StopChasingPlayer();
        }

        // Continue patrolling if not chasing
        if (isPatrolling)
        {
            Patrol();
        }
    }

    private void SetNewPatrolPoint()
    {
        float x = Random.Range(roomMinBounds.x, roomMaxBounds.x);
        float z = Random.Range(roomMinBounds.z, roomMaxBounds.z);
        patrolPoint = new Vector3(x, transform.position.y, z);

        agent.SetDestination(patrolPoint);
    }

    private void Patrol()
    {
        //animator.SetTrigger("Idle");
        // Check if enemy reached the patrol point
        if (Vector3.Distance(transform.position, patrolPoint) <= 1f)
        {
            SetNewPatrolPoint();
        }
    }

    private void StartChasingPlayer()
    {
        //Debug.Log("Chasing!");
        isPatrolling = false;
        isChasing = true;
        animator.SetBool("isChasing", true);
        animator.SetTrigger("CrawlFast");
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    private void StopChasingPlayer()
    {
        isPatrolling = true;
        isChasing = false;
        agent.speed = patrolSpeed;
        SetNewPatrolPoint();
    }

    private void AttackPlayer()
    {
        animator.SetTrigger("Attack");
        animator.SetBool("isAttacking", false);
        Debug.Log("Enemy attacks the player!");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
