using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow2D : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 2.0f; // Speed at which the enemy moves
    public GameObject Enemy1;
    public bool canSpawn = false;

    private void Start()
    {
        StartCoroutine(Spawner());
    }
    void Update()
    {    //Enemy f�ljer efter spelaren
        if (player != null)
        {
            // R�kna ut "direction" fr�n spelaren
            Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
            direction.Normalize(); // Normalize the direction vector

            // Fiende r�r sig mot spelare
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Fiende kolla p� spelare
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}