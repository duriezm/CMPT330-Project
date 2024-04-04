using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallPartCollider : MonoBehaviour
{
    private void onTriggerEnter(Collider other) 
    {
        PartInventory partInventory = other.GetComponent<PartInventory>();

        if (partInventory != null)
        {
            partInventory.smallCollected();
            gameObject.SetActive(false);
        }
    }
}
