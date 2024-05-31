using UnityEngine;
using System.Collections;

public class BossBodySlam : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public SpriteRenderer spriteRenderer; // Sprite Renderer to change sprites when jumping
    public Sprite normalSprite; // Normal sprite
    public Sprite jumpingSprite; // Sprite for when jumping
    public float speed = 2.0f; // Speed at which the boss moves
    public float normalPushForce = 1.5f; // Base force to apply when pushing normally
    public float enhancedPushForce = 22.5f; // Base force multiplied by 15 for enhanced push
    public float jumpDuration = 3f; // Time boss stays "in the air"
    public float pushDuration = 0.5f; // Duration for which the push force is applied
    public float normalPushCooldown = 3f; // Cooldown duration for normal pushes
    public float enhancedPushCooldown = 10f; // Cooldown duration for enhanced pushes

    private float normalPushTimer = 0f; // Timer to track normal push duration and cooldown
    private float enhancedPushTimer = 0f; // Timer to track enhanced push duration and cooldown
    private bool isPushing = false; // Flag to indicate if pushing is active
    private bool canPush = true; // Flag to indicate if the boss can push
    private Rigidbody2D rb; // Rigidbody component of the player for force application

    private void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        if (player == null)
        {
            player = FindObjectOfType<Movement>().transform; // Ensure this fetches the correct player movement script component
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
            direction.Normalize(); // Normalize the direction vector

            if (!isPushing)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }

            UpdatePushTimers();
        }
    }

    private void UpdatePushTimers()
    {
        if (normalPushTimer > 0)
        {
            normalPushTimer -= Time.deltaTime;
        }
        if (enhancedPushTimer > 0)
        {
            enhancedPushTimer -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            if (enhancedPushTimer <= 0 && canPush)
            {
                StartCoroutine(JumpAndPush());
                enhancedPushTimer = enhancedPushCooldown;
                canPush = false;
            }
            else if (normalPushTimer <= 0 && canPush)
            {
                NormalPush();
                normalPushTimer = normalPushCooldown;
                canPush = false;
            }
        }
    }

    IEnumerator JumpAndPush()
    {
        isPushing = true;
        spriteRenderer.sprite = jumpingSprite;
        yield return new WaitForSeconds(jumpDuration);
        spriteRenderer.sprite = normalSprite;
        EnhancedPush();
        isPushing = false;
    }

    void NormalPush()
    {
        ApplyPush(normalPushForce);
        Invoke("ResetPush", normalPushCooldown);
    }

    void EnhancedPush()
    {
        ApplyPush(enhancedPushForce);
        Invoke("ResetPush", enhancedPushCooldown);
    }

    void ApplyPush(float force)
    {
        if (rb != null)
        {
            Vector2 pushDirection = ((Vector2)player.position - (Vector2)transform.position).normalized;
            rb.velocity = Vector2.zero; // Reset the player's velocity
            rb.AddForce(pushDirection * force, ForceMode2D.Impulse);

            Debug.Log("Pushed player with force: " + force);
        }
        else
        {
            Debug.LogWarning("Player does not have a Rigidbody2D component.");
        }
    }

    void ResetPush()
    {
        canPush = true;
    }
}
