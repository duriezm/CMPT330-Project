using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPartCollider : MonoBehaviour
{
    private void onTriggerEnter(Collider other) 
    {
        PartInventory partInventory = other.GetComponent<PartInventory>();

        Debug.Log(other);
        Debug.Log(partInventory);

        if (partInventory != null)
        {
            partInventory.smallCollected();
            Debug.Log(gameObject);
            Destroy(gameObject);
        }
    }
}
