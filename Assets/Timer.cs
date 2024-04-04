using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timeText;

    float elapsed;

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        int min = Mathf.FloorToInt(elapsed / 60);
        int sec = Mathf.FloorToInt(elapsed % 60);
        timeText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
