using UnityEngine;

public class Movement : MonoBehaviour
{
    public StatApplier statApplier;
    public float moveSpeed;
    public Camera mainCamera; // Reference to the main camera

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isBeingPushed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 1f + statApplier.speedMod;
    }

    void Update()
    {
        rb.drag = 0.05f + statApplier.weightMod;

        // Input handling for movement only if not being pushed
        if (!isBeingPushed)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Normalizing the vector to maintain consistent speed
            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }
        }
    }

    void FixedUpdate()
    {
        // Applying movement to the player only if not being pushed
        if (!isBeingPushed)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        // If being pushed, let the physics engine handle the movement based on forces applied
    }

    public void StartPushImpact(float duration, Vector2 pushForce)
    {
        if (!isBeingPushed) // Check to prevent re-setting the force if already being pushed
        {
            isBeingPushed = true;
            rb.AddForce(pushForce, ForceMode2D.Impulse); // Apply the external push force
            Invoke("EndPushImpact", duration); // Automatically end the impact after a duration
        }
    }

    private void EndPushImpact()
    {
        isBeingPushed = false;
        rb.velocity = Vector2.zero; // Optionally reset the velocity after the push effect ends
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            Debug.Log("Player exits arena, dies.");

            Destroy(gameObject);
        }
    }
}
