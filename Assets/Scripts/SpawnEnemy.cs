using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // enemy spawn position
    Transform[] spawnPositions;
    // count of enemy spawn positions
    int spawnCount;
    // enemy spawn min. time
    public float spawnTimeMin = 1f;
    // enemy spawn max. time
    public float spawnTimeMax = 10f;
    // enemy spawn interval
    float spawnInterval;
    // time after spawn enemy
    float spawnTime;
    // enemy prefabs
    public GameObject[] prefabEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        spawnPositions = GetComponentsInChildren<Transform>();
        spawnCount = spawnPositions.Length;
        //print(spawnCount);
        // spawn enemy
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate spawn time
        spawnTime += Time.deltaTime;
        // spawn enemy
        if (spawnTime > spawnInterval) 
        {
            Spawn();
        }
    }

    void Spawn()
    {
        // enemy spawn position
        int pos = Random.Range(1, spawnCount);
        //print(pos);
        // index of enemies
        int countEnemy = Random.Range(0, prefabEnemies.Length);
        // instantiate enemy
        Instantiate(prefabEnemies[countEnemy], spawnPositions[pos].position, spawnPositions[pos].rotation);
        // calculate spawn interval
        spawnInterval = Random.Range(spawnTimeMin, spawnTimeMax);
        // reset spawn time
        spawnTime = 0f;
        // reduce spawnTimeMax
        spawnTimeMax = spawnTimeMax * 0.95f;
        if (spawnTimeMax <= spawnTimeMin)
        {
            spawnTimeMax = spawnTimeMin;
        }
        //print(spawnTimeMax);
    }
}
