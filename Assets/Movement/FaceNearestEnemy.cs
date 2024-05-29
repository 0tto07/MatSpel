using UnityEngine;

public class FaceNearestEnemy : MonoBehaviour
{
    public float pushForce = 10f; // Force to apply when pushing
    public float minimumDistanceToEnemy = 1f; // Minimum distance to stop moving towards the enemy
    public float speed = 2.0f; // Speed at which the player moves

    private Transform nearestEnemy; // Reference to the nearest enemy

    void Update()
    {
        FindNearestEnemy();

        // Check for mouse button input to push
        if (Input.GetMouseButtonDown(0) && nearestEnemy != null && Vector2.Distance(transform.position, nearestEnemy.position) <= minimumDistanceToEnemy)
        {
            Push();
        }

        FollowAndFaceNearestEnemy();
    }

    void FindNearestEnemy()
    {
        // Find all enemies with the tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // If there are no enemies, return
        if (enemies.Length == 0)
        {
            nearestEnemy = null;
            return;
        }

        // Find the nearest enemy
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
    }

    void FollowAndFaceNearestEnemy()
    {
        if (nearestEnemy == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, nearestEnemy.position);

        // Only move towards the enemy if it is outside the minimum distance
        if (distance > minimumDistanceToEnemy)
        {
            Vector2 direction = (nearestEnemy.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, nearestEnemy.position, speed * Time.deltaTime);

            // Rotate to face the enemy
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            // Rotate to face the enemy even when within the minimum distance
            Vector2 direction = (nearestEnemy.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Push()
    {
        // Define the push direction based on the player's current rotation
        Vector2 pushDirection = transform.up; // Assuming up is the forward direction

        // Apply force to the nearest enemy if within the minimum distance
        if (nearestEnemy != null && Vector2.Distance(transform.position, nearestEnemy.position) <= minimumDistanceToEnemy)
        {
            Rigidbody2D rb = nearestEnemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply force to the enemy
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a line to visualize the push direction
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 1f);

        // Draw a sphere to visualize the minimum distance to detect and interact with an enemy
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minimumDistanceToEnemy);
    }
}
