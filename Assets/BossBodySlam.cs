using UnityEngine;
using System.Collections;

public class BossBodySlam : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public SpriteRenderer spriteRenderer; // Boss's sprite renderer
    public Sprite normalSprite; // Normal sprite when boss is on the ground
    public Sprite jumpingSprite; // Sprite when boss is 'jumping' or in the air
    public float moveSpeed = 2.0f; // Speed at which the boss moves towards the player
    public float slamForce = 100f; // Force applied when slamming
    public float slamRadius = 3.0f; // Radius of effect for the slam
    public float slamCooldown = 10f; // Cooldown duration for slam

    private Rigidbody2D rb; // Rigidbody component
    private bool isSlamming = false; // Is currently performing a slam
    private float slamTimer = 0f; // Timer to track cooldown

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = FindObjectOfType<Movement>().transform;
        }
    }

    void Update()
    {
        // Cooldown timer
        if (slamTimer > 0)
        {
            slamTimer -= Time.deltaTime;
        }

        // Movement towards the player
        if (!isSlamming && player != null)
        {
            Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
            direction.Normalize(); // Normalize the direction vector
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        // Check if it's time to start a slam
        if (!isSlamming && slamTimer <= 0 && Vector2.Distance(transform.position, player.position) < 5.0f)
        {
            StartCoroutine(PerformSlam());
            slamTimer = slamCooldown; // Reset cooldown timer
        }
    }

    IEnumerator PerformSlam()
    {
        isSlamming = true;

        // Change sprite to indicate jumping
        spriteRenderer.sprite = jumpingSprite;

        // Wait while "up in the air"
        yield return new WaitForSeconds(3);

        // Change sprite back to normal
        spriteRenderer.sprite = normalSprite;

        // Execute the slam impact
        ExecuteSlamImpact();

        // End of slam action
        isSlamming = false;
    }

    void ExecuteSlamImpact()
    {
        // Check for impact
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, slamRadius);
        foreach (var hit in hitColliders)
        {
            if (hit.transform == player)
            {
                // Apply force if it's the player
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    Vector2 direction = (player.position - transform.position).normalized;
                    playerRb.AddForce(direction * slamForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // To visualize the slam effect radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, slamRadius);
    }
}
