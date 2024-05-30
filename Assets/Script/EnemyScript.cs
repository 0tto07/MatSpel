using UnityEngine;

public class EnemyFollow2D : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float speed = 2.0f; // Speed at which the enemy moves
    public float pushForce = 1.5f; // Force to apply when pushing
    public float pushDuration = 0.5f; // Duration for which the push force is applied
    public float pushCooldown = 3f; // Cooldown duration before another push can occur

    private float pushTimer = 0f; // Timer to track push duration and cooldown
    private bool isPushing = false; // Flag to indicate if pushing is active
    private bool canPush = true; // Flag to indicate if the enemy can push

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
            direction.Normalize(); // Normalize the direction vector

            // Enemy moves towards player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Enemy faces the player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Update push timer
            if (isPushing || !canPush)
            {
                pushTimer += Time.deltaTime;

                // Check if push is active
                if (isPushing && pushTimer >= pushDuration)
                {
                    isPushing = false;
                    pushTimer = 0f; // Reset timer for cooldown
                }

                // Check if push is cooling down
                if (!canPush && pushTimer >= pushCooldown)
                {
                    canPush = true;
                    pushTimer = 0f; // Reset timer for next push
                }
            }

            // Check if the enemy can push
            if (canPush && !isPushing && Vector2.Distance(transform.position, player.position) < 2.0f) // assuming a push range of 2 units
            {
                Push();
            }
        }
    }

    void Push()
    {
        if (player != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Calculate push direction (away from the enemy)
                Vector2 pushDirection = ((Vector2)player.position - (Vector2)transform.position).normalized;
                rb.velocity = Vector2.zero; // Reset the player's velocity
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                isPushing = true;
                canPush = false; // Disable further pushes until cooldown is over
                Debug.Log("Pushed player with force: " + pushDirection * pushForce);
            }
            else
            {
                Debug.LogWarning("Player does not have a Rigidbody2D component.");
            }
        }
    }
}
