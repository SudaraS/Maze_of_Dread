using UnityEngine;
using UnityEngine.AI;

public class CrawlerController : MonoBehaviour
{
    public float wanderRadius = 1f; // Radius within the room for random wandering
    public float idleTime = 2f; // Time in seconds to idle between movements
    public float detectionRange = 10f; // Distance at which the enemy will detect the player
    public Transform player; // Reference to the player

    private NavMeshAgent agent;
    private float idleTimer;
    private bool isIdling = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        idleTimer = idleTime;

        // Check if the agent starts on a valid NavMesh area
        if (!agent.isOnNavMesh)
        {
            Debug.LogWarning("NavMeshAgent is not on the NavMesh. Please ensure the enemy is positioned on a valid NavMesh area.");
        }
    }

    void Update()
    {
        // Only proceed if the agent is on the NavMesh
        if (!agent.isOnNavMesh) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // Chase the player if within detection range
            agent.SetDestination(player.position);
        }
        else
        {
            if (isIdling)
            {
                // Count down idle time
                idleTimer -= Time.deltaTime;

                if (idleTimer <= 0)
                {
                    // Stop idling and start wandering
                    isIdling = false;
                    Wander();
                }
            }
            else
            {
                // Check if agent has reached its destination
                if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
                {
                    // Start idling
                    isIdling = true;
                    idleTimer = idleTime;
                }
            }
        }
    }

    void Wander()
    {
        // Find a random point within the wander radius
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        // Ensure the point is on the NavMesh
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(navHit.position);
        }
    }
}
