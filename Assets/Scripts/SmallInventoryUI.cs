using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SmallInventoryUI : MonoBehaviour
{
    private TextMeshProUGUI smallCount;
    // Start is called before the first frame update
    void Start()
    {
        smallCount = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateSmallText(PartInventory partInventory)
    {
        smallCount.text = partInventory.smallParts.ToString();
    }

}
