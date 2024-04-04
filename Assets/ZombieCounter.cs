using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ZombieCounter : MonoBehaviour
{
    public TextMeshProUGUI zombieKilledText;
    public float zombieKilled;
    // Start is called before the first frame update
    void Start()
    {
        zombieKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementZombieCounter()
    {
        zombieKilled++;
        zombieKilledText.SetText(zombieKilled.ToString());
    }
}
