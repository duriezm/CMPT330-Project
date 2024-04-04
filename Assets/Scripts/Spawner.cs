using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;

    [SerializeField]
    private float zombieInterval = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(zombieInterval, zombie));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newZombie = Instantiate(enemy, new Vector3(Random.Range(600f, 630f), 6.5f, Random.Range(250f, 300f)), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}