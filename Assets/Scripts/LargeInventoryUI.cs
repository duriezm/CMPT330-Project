using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LargeInventoryUI : MonoBehaviour
{
    private TextMeshProUGUI largeCount;
    // Start is called before the first frame update
    void Start()
    {
        largeCount = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateLargeText(PartInventory partInventory)
    {
        largeCount.text = partInventory.largeParts.ToString();
    }
}
