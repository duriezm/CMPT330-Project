using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{   
    [SerializeField]
    private List<GameObject> zombies;

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

    private int[] table = {50, 25, 20, 5}; //spawn chances of zombies

    private int total;



    // Start is called before the first frame update
    void Start()
    {
        foreach(var item in table)
        {
            total += item;
        }
        StartCoroutine(spawnEnemy(zombieInterval));
        
    }

    private IEnumerator spawnEnemy(float interval)
    {
        yield return new WaitForSeconds(interval);
        GameObject newZombie = getZombie();
        StartCoroutine(spawnEnemy(interval));
    }

    private GameObject getZombie() {
        int randomNum = Random.Range(0, total);
        GameObject newZombie = zombies[0];
        for (int i = 0; i < table.Length; i++) {
            if (randomNum <= table[i]) {
                newZombie = Instantiate(zombies[i], new Vector3(Random.Range(spawnXStart, spawnXEnd), 6.5f, Random.Range(spawnZStart, spawnZEnd)), Quaternion.identity);
                return newZombie;
            }
            else 
            {
                randomNum -= table[i];
            }
        }
        return newZombie;
    }

}