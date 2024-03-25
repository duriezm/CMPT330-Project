using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmAttack : MonoBehaviour
{
    //public float armTimer;
    public void OnCollisionEnter(Collision collision)
    {
        //armTimer += Time.deltaTime;
        Transform hitTransform = collision.transform;
        //if (hitTransform.CompareTag("Player") && armTimer > .25)
        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(2);
            //armTimer = 0;
        }
    }
}
