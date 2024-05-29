using UnityEngine;

public class FaceNearestEnemy : MonoBehaviour
{
    void Update()
    {
        FaceNearestEnemyInScene();
    }

    void FaceNearestEnemyInScene()
    {
        // Find all enemies with the tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // If there are no enemies, return
        if (enemies.Length == 0)
        {
            return;
        }

        // Find the nearest enemy
        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        // If a nearest enemy is found, rotate towards it
        if (nearestEnemy != null)
        {
            Vector2 directionToEnemy = (nearestEnemy.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
