using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public VictoryScript win;
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public Transform spawnPoint; // The specific position where the enemy will be spawned
    private GameObject currentEnemy; // Reference to the currently spawned enemy
    WaitForSeconds wait;
    bool spawned = true;

    void Start()
    {
        StartCoroutine(Spawner());
        SpawnEnemy();

    }

    private IEnumerator Spawner()
    {
        
        if (spawned == false)
        {
            wait = new WaitForSeconds(10);
            yield return wait;
            SpawnEnemy() ;
        }
    }

    void Update()
    {
        
        if ( currentEnemy == null)
        {
            win.OnWin();
        }
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
        spawned = true;
    }
}

