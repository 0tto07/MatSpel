using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform spawnPoint; // The specific position where the enemy will be spawned
    private GameObject currentEnemy; // Reference to the currently spawned enemy

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        // Check if the current enemy is null (i.e., has been destroyed)
        if (currentEnemy == null)
        {
            

            SpawnEnemy();
        }
    }
    

    void SpawnEnemy()
    {
        // Spawn the enemy at the spawn point's position
        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}

