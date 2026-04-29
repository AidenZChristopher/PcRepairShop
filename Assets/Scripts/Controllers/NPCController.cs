using UnityEngine;
using UnityEngine.AI;

// This script was made based off a youtube video by Walter Redi / Unity Game Development called NPC Waypoint Pathfinding (3 MINUTES) - Unity Game Dev Tutorial, https://www.youtube.com/watch?v=jGTx7Lq7aak
public class MoveNPC : MonoBehaviour
{
    [SerializeField] Transform[] destinations;
    [SerializeField] float minWaitTime = 2f;
    [SerializeField] float maxWaitTime = 5f;

    NavMeshAgent navMeshAgent;
    Animator animator;

    private static readonly int IsMovingHash = Animator.StringToHash("isMoving");

    int currentDestinationIndex = 0;
    bool isWaiting = false;
    bool isMoving = false;
    float waitTimer = 0f;
    float currentWaitTime = 0f;

    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();

        if (navMeshAgent == null)
            Debug.LogError("[MoveNPC] nav mesh agent component not attached");

        if (animator == null)
            Debug.LogError("[MoveNPC] animator component not attached");

        SetDestination();
    }

    void Update()
    {
        if (navMeshAgent == null || animator == null) return;

        UpdateAnimation();

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= currentWaitTime)
            {
                isWaiting = false;
                AdvanceToNextDestination();
            }
        }
        else
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                StartWaiting();
            }
        }
    }

    private void UpdateAnimation()
    {
        bool agentIsMoving = !navMeshAgent.pathPending && navMeshAgent.velocity.magnitude > 0.1f;

        if (isMoving == false && agentIsMoving)
        {
            isMoving = true;
            Debug.Log("[MoveNPC] Playing Walking animation");
            animator.SetBool(IsMovingHash, true);
        }

        if (isMoving && !agentIsMoving)
        {
            isMoving = false;
            Debug.Log("[MoveNPC] Playing Idle animation");
            animator.SetBool(IsMovingHash, false);
        }
    }

    private void SetDestination()
    {
        if (destinations == null || destinations.Length == 0)
        {
            Debug.LogError("[MoveNPC] no destinations assigned");
            return;
        }

        navMeshAgent.SetDestination(destinations[currentDestinationIndex].position);
    }

    private void StartWaiting()
    {
        isWaiting = true;
        waitTimer = 0f;
        currentWaitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    private void AdvanceToNextDestination()
    {
        currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Length;
        SetDestination();
    }
}