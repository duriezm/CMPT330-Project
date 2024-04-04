using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumPartCollider : MonoBehaviour
{
    private void onTriggerEnter(Collider other) 
    {
        PartInventory partInventory = other.GetComponent<PartInventory>();

        if (partInventory != null)
        {
            partInventory.mediumCollected();
            gameObject.SetActive(false);
        }
    }
}
