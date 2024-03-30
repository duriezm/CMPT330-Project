using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;

    // debugging purposes
    [SerializeField]
    [Header("Sight Values")]
    public float sightDistance = 40f;
    public float fieldOfView = 180f;
    public float eyeHeight = 1.1f;
    [Header("Weapon Values")]
    public Transform leftarm;
    public Transform rightarm;
    [Range(0.1f, 1f)]
    Transform target;
    private float speed = 5f;

    // used to set animation
    private Animator animator;

    // track which waypoint we are currently targeting
    private int waypointIndex;
    private float waitTimer;

    // waypoints to patrol
    public List<Transform> waypoints = new List<Transform>();

    // 0 is uninitalized or idle
    // 1 is walking, 2 is attacking
    public int state = 0;

    private int zombieHealth;
    public int maxHealth=100;
    //bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("ZombieContinueWalk", true);
        animator.SetBool("GoIdleToStayIdle", true);
        state = 0;
        zombieHealth = maxHealth;
        //isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (isDead)
        //{
        //    print("Zombie is dead");
        //    return;
        //}
        // seeing player is true, and will move towards target
        if (CanSeePlayer())
        {
            // transition animation from one state to another, state=0 idle, state=1 walking, state=2 attacking
            // idle to attack
            if (state == 0)
            {
                animator.SetBool("ZombieContinueWalk", false);
                animator.SetTrigger("StartMove");
                state = 2;
            }
            // walk to attack
            else if (state == 1)
            {
                animator.SetTrigger("Attack");
                state = 2;
            }
            // pause patrol if zombie is patrolling
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            MoveTowardsPlayer();
        }
        else
        {
            // transition animation from one state to another, state=0 idle, state=1 walking, state=2 attacking
            // idle to walk
            if (state == 0)
            {
                animator.SetBool("ZombieContinueWalk", true);
                animator.SetTrigger("StartMove");
                state = 1;
            }
            // attack to walk
            else if (state == 2)
            {
                animator.SetTrigger("LostSightBackToPatrol");
                state = 1;
            }
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

    public void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 1.25f)
        {
            // move zombie closer to player and how quickly he closes the distance
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        }


        // Check the position of the zombie and player and compare
        //if (Vector3.Distance(transform.position, player.transform.position) < 0.0001f)
        //{
        //    player.transform.position *= -1f;
        //}

        return;
    }

    public void PatrolState()
    {
        if (agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 1)
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
    public void TakeDamage(int damage)
    {
        zombieHealth -= damage;
        if (zombieHealth <= 0)
        {
            //isDead = true;
            // lets test
            Destroy(gameObject);
        }
    }
}
