using UnityEngine;

public class FaceNearestEnemy : MonoBehaviour
{
    public float pushForce = 10f; // Force to apply when pushing

    void Update()
    {
        FaceNearestEnemyInScene();

        // Check for mouse button input to push
        if (Input.GetMouseButtonDown(0))
        {
            Push();
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

    void Push()
    {
        // Define the push direction based on the player's current rotation
        Vector2 pushDirection = transform.up; // Assuming up is the forward direction

        // Perform a raycast to detect objects in the push direction
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, pushDirection, 1f);

        foreach (var hit in hits)
        {
            // Ensure the object has a Rigidbody2D and is not the player itself
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                Rigidbody2D rb = hit.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Apply force to the object
                    rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a line to visualize the push direction
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
    }
}
