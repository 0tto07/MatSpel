using UnityEngine;
using System.Collections;

public class BossBodySlam : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public SpriteRenderer spriteRenderer; // Sprite Renderer to change sprites when jumping and landing
    public Sprite normalSprite; // Normal sprite
    public Sprite preJumpSprite; // Sprite for pre-jump
    public Sprite jumpingSprite; // Sprite for when jumping
    public Sprite landingSprite; // Sprite for when landing
    public float speed = 2.0f; // Speed at which the boss moves
    public float normalPushForce = 1.5f; // Base force to apply when pushing normally
    public float enhancedPushForce = 22.5f; // Base force multiplied by 15 for enhanced push
    public float preJumpDuration = 1f; // Time before jump while in pre-jump state
    public float jumpDuration = 3f; // Time boss stays "in the air"
    public float landingDuration = 1f; // Time boss stays in landing sprite
    public float pushDuration = 0.5f; // Duration for which the push force is applied
    public float normalPushCooldown = 3f; // Cooldown duration for normal pushes
    public float enhancedPushCooldown = 10f; // Cooldown duration for enhanced pushes

    private float normalPushTimer = 0f; // Timer to track normal push duration and cooldown
    private float enhancedPushTimer = 0f; // Timer to track enhanced push duration and cooldown
    private bool isPushing = false; // Flag to indicate if pushing is active
    private bool canPush = true; // Flag to indicate if the boss can push
    private Rigidbody2D rb; // Rigidbody component of the boss

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
        if (player != null)
        {
            Vector2 direction = (Vector2)player.position - (Vector2)transform.position;
            direction.Normalize();
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        UpdatePushTimers();

        if (!isPushing && canPush)
        {
            Debug.Log("Attempting to jump");
            StartCoroutine(PreJumpAndLand());
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
        if (collision.gameObject == player.gameObject && isPushing)
        {
            Debug.Log("Collided with player during push");
            ApplyPush(enhancedPushForce);
            isPushing = false;
        }
    }

    IEnumerator PreJumpAndLand()
    {
        Debug.Log("Starting jump sequence");
        isPushing = true;
        canPush = false;

        // Change to pre-jump sprite
        spriteRenderer.sprite = preJumpSprite;
        yield return new WaitForSeconds(preJumpDuration);

        // Change to jumping sprite
        spriteRenderer.sprite = jumpingSprite;
        yield return new WaitForSeconds(jumpDuration);

        // Change to landing sprite
        spriteRenderer.sprite = landingSprite;
        yield return new WaitForSeconds(landingDuration);

        // Change back to normal sprite
        spriteRenderer.sprite = normalSprite;
        isPushing = false;
        Debug.Log("Ending jump sequence");
        Invoke("ResetPush", enhancedPushCooldown);
    }

    void NormalPush()
    {
        ApplyPush(normalPushForce);
        Invoke("ResetPush", normalPushCooldown);
    }

    void ApplyPush(float force)
    {
        Movement playerMovement = player.GetComponent<Movement>();
        if (playerMovement != null)
        {
            Vector2 pushDirection = ((Vector2)player.position - (Vector2)transform.position).normalized;
            playerMovement.StartPushImpact(pushDuration, pushDirection * force);

            Debug.Log("Pushed player with force: " + force);
        }
        else
        {
            Debug.LogWarning("Player does not have the Movement component.");
        }
    }

    void ResetPush()
    {
        Debug.Log("Resetting push");
        canPush = true;
    }
}
