using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(NavMeshAgent))]
public class SimpleAI : MonoBehaviour {
    [Header("Agent Field of View Properties")]
    [SerializeField] private float viewRadius;
    [SerializeField] private float viewAngle;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;

    [Space(5)]
    [Header("Agent Properties")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float patrolRadius;


    private Transform playerTarget;

    private Vector3 currentDestination;

    private bool playerSeen;

    private int maxNumberOfNewDestinationBeforeDeath;
    private enum State {Wandering, Chasing};
    private State currentState;

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;

    public GameObject player;

    // Use this for initialization
    void Start () {
        currentDestination = RandomNavSphere(transform.position, patrolRadius, -1);
        maxNumberOfNewDestinationBeforeDeath = Random.Range(5, 50);

        animator = transform.GetComponentInChildren<Animator>();
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
    }

    private void CheckState()
    {
        FindVisibleTargets();

        switch(currentState)
        {
            case State.Chasing:
                ChaseBehavior();
                break;

            default:
                WanderBehavior();
                break;

        }
    }

    void WanderBehavior()
    {
        if (animator != null)       
            animator.SetTrigger("walk");
        navMeshAgent.speed = walkSpeed;

        float dist = navMeshAgent.remainingDistance;

        if (dist != Mathf.Infinity && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            currentDestination = RandomNavSphere(transform.position, patrolRadius, -1);
            navMeshAgent.SetDestination(currentDestination);
            maxNumberOfNewDestinationBeforeDeath--;
            if (maxNumberOfNewDestinationBeforeDeath <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void ChaseBehavior()
    {
        if (playerTarget != null)
        {
            if (animator != null)
                animator.SetTrigger("run");
            navMeshAgent.speed = runSpeed;
            currentDestination = playerTarget.transform.position;
            navMeshAgent.SetDestination(currentDestination);
        }
        else
        {
            playerSeen = false;
            currentState = State.Wandering;
        }
    }

     private void OnTriggerEnter(Collider other)
     {
         if (other.CompareTag("Player"))
         {
            HasFindPlayer();
         }
     }

    #region Vision
    void FindVisibleTargets()
    {

        playerTarget = null;
        playerSeen = false;
      
       
        Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dirToTarget, out hit))
        {
            float dstToTarget = Vector3.Distance(transform.position, player.transform.position);
            if (dstToTarget <= viewRadius)
            {
                if (Vector3.Angle(transform.forward, dirToTarget) <= viewAngle / 2)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        playerSeen = true;
                        playerTarget = hit.transform;
                    }
                }
            }
        }
        
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    #endregion

    private void HasFindPlayer()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    
    // Update is called once per frame
    void Update () {
        CheckState();

        if (playerSeen)
        {
            currentState = State.Chasing;
        }
        else
        {
            currentState = State.Wandering;
        }
	}
}
