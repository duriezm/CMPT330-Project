using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargePartCollider : MonoBehaviour
{
    private void onTriggerEnter(Collider other) 
    {
        PartInventory partInventory = other.GetComponent<PartInventory>();

        if (partInventory != null)
        {
            partInventory.largeCollected();
            gameObject.SetActive(false);
        }
    }
}
