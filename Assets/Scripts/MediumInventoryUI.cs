using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MediumInventoryUI : MonoBehaviour
{
    private TextMeshProUGUI mediumCount;
    // Start is called before the first frame update
    void Start()
    {
        mediumCount = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateMediumText(PartInventory partInventory)
    {
        mediumCount.text = partInventory.mediumParts.ToString();
    }
}
