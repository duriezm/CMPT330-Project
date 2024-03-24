using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    // debugging purposes
    [SerializeField]
    [Header("Sight Values")]
    public float sightDistance = 30f;
    public float fieldOfView = 85f;
    public float eyeHeight = 1.1f;
    [Header("Weapon Values")]
    [Range(0.1f, 1f)]
    public float attackRate;
    Transform target;
    private float speed = 2f;

    // used to set animation
    private Animator animator;

    // track which waypoint we are currently targeting
    private int waypointIndex;
    private float waitTimer;

    // timer for when zombie loses sight
    private float lostSightTimer;


    // waypoints to patrol
    public List<Transform> waypoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("ZombieContinueWalk", true);
        animator.SetBool("GoIdleToStayIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        
        // seeing player is true, and will move towards target
        if (CanSeePlayer())
        {
            lostSightTimer = 0;
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            if (MoveTowardsPlayer())
            {
                ArmAttack();
            }
        }
        else
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            PatrolState();
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
                            // have zombie adjust facing to player
                            agent.transform.LookAt(player.transform);
                            // in scene, you can see we use ray tracing to get player lock on
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;                            
                        }
                    }
                }
            }
        }
        return false;
    }

    public bool MoveTowardsPlayer()
    {
        // move zombie closer to player and how quickly he closes the distance
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);



        // Check the position of the zombie and player and compare
        //if (Vector3.Distance(transform.position, player.transform.position) < 0.0001f)
        //{
        //    player.transform.position *= -1f;
        //}

        return true;
    }

    public void ArmAttack()
    {

    }

    public void AttackState()
    {

    }
    public void IdleState()
    {

    }
    public void PatrolState()
    {
        if (agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 2)
            {
                if (waypointIndex < waypoints.Count - 1)
                {
                    waypointIndex++;
                }
                else
                {
                    waypointIndex = 0;
                }
                agent.SetDestination(waypoints[waypointIndex].position);
                waitTimer = 0;
            }
        }
    }
}
