using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie;

    [SerializeField]
    private float zombieInterval = 5.0f;

    [SerializeField]
    private float spawnXStart = 0.0f;

    [SerializeField]
    private float spawnXEnd = 0.0f;
    
    [SerializeField]
    private float spawnZStart = 0.0f;

    [SerializeField]
    private float spawnZEnd = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(zombieInterval, zombie));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newZombie = Instantiate(enemy, new Vector3(Random.Range(spawnXStart, spawnXEnd), 6.5f, Random.Range(spawnZStart, spawnZEnd)), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}