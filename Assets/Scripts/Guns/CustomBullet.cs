using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    //assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    //stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    // damage
    public int explosionDamage;
    public float explosionRange;

    //lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    // Start is called before the first frame update
    private void Start()
    {
        Setup();
    }
    // Update is called once per frame
    private void Update()
    {
        //when to explode
        if (collisions > maxCollisions)
        {
            Explode();
        }

        //count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0)
        {
            Explode();
        }
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //count number of collisions, used for bouncy bullets
        collisions++;

        //explode if bullet hits an enemy directly and explodeOnTouch is activated
        if (collision.collider.CompareTag("Zombie") && explodeOnTouch)
        {
            Explode();
        }
    }
    private void Setup()
    {
        //create a new physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;

        //set gravity
        rb.useGravity = useGravity;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
    private void Explode()
    {
        //instantiate explosion
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        // add a little delay, just to make sure everything works fine
        Invoke("Delay", 0.05f);

    }
}
