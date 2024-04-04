using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Processors;
using UnityEngine.XR;

public class EnemyTest : MonoBehaviour
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

    // speed of zombie
    private float speed = 5f;

    // used to set animation
    private Animator animator;

    // 0 is uninitalized or idle
    // 1 is walking, 2 is attacking
    public int state = 0;

    // zombie health
    private int zombieHealth;
    public int zombieMaxHealth = 100;

    //healthbar
    [SerializeField]
    FloatingHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("ZombieContinueWalk", true);
        animator.SetBool("GoIdleToStayIdle", true);
        state = 0;
        zombieHealth = zombieMaxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHeathBar(zombieHealth, zombieMaxHealth);
    }
    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            //gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            MoveTowardsPlayer();
        }
        else
        {
            // transition animation from one state to another, state=0 idle, state=1 walking, state=2 attacking
            // attack to walk
            if (state == 2)
            {
                animator.SetTrigger("LostSightBackToPatrol");
                state = 1;
                //gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            }
            //gameObject.GetComponent<NavMeshAgent>().isStopped = false;
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

        return;
    }

    public void TakeDamage(int damage)
    {
        print(zombieHealth);
        zombieHealth -= damage;
        healthBar.UpdateHeathBar(zombieHealth, zombieMaxHealth);
        if (zombieHealth <= 0)
        {
            //lets set zombie to an animimation to dead, later implementation if time applicable
            Destroy(gameObject);
        }
        print(zombieHealth);
        return;
    }
}
