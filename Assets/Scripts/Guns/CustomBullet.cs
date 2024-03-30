using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    //assignables
    public Rigidbody rb;

    // damage
    public int gunDamage;

    //lifetime
    public float maxLifetime;


    // Start is called before the first frame update
    private void Start()
    {
        
    }
    // Update is called once per frame
    private void Update()
    {
        //count down lifetime
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //explode if bullet hits an enemy directly and explodeOnTouch is activated
        if (collision.collider.CompareTag("Zombie"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(gunDamage);
            print("Zombie has been hit");
        }
        Destroy(gameObject);
    }
}
