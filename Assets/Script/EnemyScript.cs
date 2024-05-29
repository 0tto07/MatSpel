using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow2D : MonoBehaviour
{
    [SerializeField] float minimumDistanceToPlayer = 0.1f;

    Transform player; // Reference to the player's transform
    public float speed = 2.0f; // Speed at which the enemy moves
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    private GameObject currentEnemy;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (minimumDistanceToPlayer > Vector2.Distance(transform.position, player.position))
        {
            return;
        }

        //Enemy följer efter spelaren
        if (player != null)
        {
            // Räkna ut "direction" från spelaren
            Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
            direction.Normalize(); // Normalize the direction vector

            // Fiende rör sig mot spelare
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Fiende kolla på spelare
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
