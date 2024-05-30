using UnityEngine;
using System.Collections;

public class FaceNearestEnemy : MonoBehaviour
{
    public float pushForce = 10f; // Force to apply when pushing
    private Transform nearestEnemy; // Reference to the nearest enemy
    private bool isPushing = false; // Flag to indicate if pushing is active
    private float pushDuration = 1f; // Duration for which the push force is applied
    private float pushTimer = 0f; // Timer to track push duration
    private bool canPush = false; // Flag to indicate if the player can push

    void Update()
    {
        FindNearestEnemy();

        // Check for mouse button input to push
        if (Input.GetMouseButtonDown(0) && canPush)
        {
            Push();
        }

        FollowAndFaceNearestEnemy();

        // Handle push timer
        if (isPushing)
        {
            pushTimer += Time.deltaTime;
            if (pushTimer >= pushDuration)
            {
                isPushing = false;
                pushTimer = 0f;
            }
        }
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

        // Rotate to face the enemy
        Vector2 direction = (nearestEnemy.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // The movement towards the enemy will be handled by another script
    }

    void Push()
    {
        // Define the push direction based on the player's current rotation
        Vector2 pushDirection = transform.right; // Pushing to the right (90 degrees clockwise)

        // Apply force to the nearest enemy if colliding
        if (nearestEnemy != null)
        {
            Rigidbody2D rb = nearestEnemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply force to the enemy
                rb.velocity = Vector2.zero; // Reset the velocity before applying force
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                isPushing = true;
                StartCoroutine(StopEnemyMovement(rb, pushDuration));
                Debug.Log("Pushed enemy: " + nearestEnemy.name + " with force: " + pushDirection * pushForce);
            }
            else
            {
                Debug.LogWarning("Nearest enemy does not have a Rigidbody2D component.");
            }
        }
    }

    private IEnumerator StopEnemyMovement(Rigidbody2D enemyRb, float duration)
    {
        

        yield return new WaitForSeconds(1);
        /*float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            enemyRb.velocity = Vector2.zero;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        */
        Debug.Log("Hej ehheh");
        Vector2 originalVelocity = enemyRb.velocity;
        enemyRb.velocity = Vector2.zero;

        enemyRb.velocity = originalVelocity;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the trigger of an enemy
        if (other.CompareTag("Enemy"))
        {
            nearestEnemy = other.transform;
            canPush = true;
            Debug.Log("Entered enemy trigger: " + other.name);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the trigger of an enemy
        if (other.CompareTag("Enemy"))
        {
            canPush = false;
            Debug.Log("Exited enemy trigger: " + other.name);
        }
    }

    void OnDrawGizmos()
    {
        // Draw a line to visualize the push direction
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 1f);
    }
}
