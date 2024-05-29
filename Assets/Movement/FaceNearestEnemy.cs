using UnityEngine;

public class FaceNearestEnemy : MonoBehaviour
{
    public float pushForce = 10f; // Force to apply when pushing
    private bool isPushing = false; // Flag to determine if push is active

    void Update()
    {
        FaceNearestEnemyInScene();

        // Check for mouse button input to initiate push
        if (Input.GetMouseButtonDown(0))
        {
            isPushing = true;
        }
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
            Vector3 directionToEnemy = (nearestEnemy.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Debug logs
            Debug.Log("Nearest Enemy: " + nearestEnemy.name);
            Debug.Log("Angle: " + angle);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (isPushing)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Define the push direction based on the player's current rotation
                Vector2 pushDirection = transform.up; // Assuming up is the forward direction
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }

            // Reset the pushing flag
            isPushing = false;
        }
    }

    void OnDrawGizmos()
    {
        // Draw a line to visualize the push direction
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
}
