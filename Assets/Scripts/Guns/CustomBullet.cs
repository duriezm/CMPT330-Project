using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    //assignables
    public Rigidbody rb;

    // damage
    public int gunDamage;
    public int critDamage=100;
    public int minDamageBody=20;
    public int maxDamageBody=30;
    public int minDamageArm = 10;
    public int maxDamageArm = 20;

    //lifetime
    public float maxLifetime;

    // Start is called before the first frame update
    private void Start()
    {
        gunDamage = Random.Range(minDamageArm, maxDamageArm);
    }
    // Update is called once per frame
    private void FixedUpdate()
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
        // body shot
        if (collision.collider.CompareTag("Zombie"))
        {
            gunDamage = Random.Range(minDamageBody, maxDamageBody);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(gunDamage);
        }
        // head shot
        else if (collision.collider.CompareTag("CritHit"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(critDamage);
        }
        // head shot
        else if (collision.collider.CompareTag("WeakHit"))
        {
            gunDamage = Random.Range(minDamageArm, maxDamageArm);
            collision.gameObject.GetComponent<Enemy>().TakeDamage(gunDamage);
        }
        Destroy(gameObject);
        return;
    }
}
