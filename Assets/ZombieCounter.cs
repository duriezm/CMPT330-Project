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

    public void incrementZombieCounter()
    {
        zombieKilled++;
        zombieKilledText.SetText(zombieKilled.ToString());
    }
}
