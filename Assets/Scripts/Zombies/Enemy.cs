using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }

    // debugging purposes
    [SerializeField]
    private string currentState;
    public Path path;
    [Header("Sight Values")]
    public float sightDistance = 30f;
    public float fieldOfView = 85f;
    public float eyeHeight = 1.1f;
    [Header("Weapon Values")]
    [Range(0.1f, 1f)]
    public float attackRate;
    Transform target;
    private float speed = 2f;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("ZombieContinueWalk", true);
        animator.SetBool("GoIdleToStayIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();

        if (CanSeePlayer() == true)
        {
            // move zombie closer to player, how quickly
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

            // Check if the position of the zombie and player are equal
            if (Vector3.Distance(transform.position, player.transform.position) < 0.001f)
            {
                player.transform.position *= -1.0f;
            }
        }

    }
    // is the player close enough to be seen?
    public bool CanSeePlayer()
    {
        if (player != null)
        {
            // check to see distance to player is within parameters
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                // angle to our player
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                // check to see if they are within the angle of the enemies view (FOV)
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    // check if view is blocked by an object
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);


                            return true;                            
                        }
                    }
                }
            }
        }
        return false;
    }
}
