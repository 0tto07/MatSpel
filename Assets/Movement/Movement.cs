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
    }

    public void StartPushImpact(float duration)
    {
        isBeingPushed = true;
        Invoke("EndPushImpact", duration); // Automatically end the impact after a duration
    }

    private void EndPushImpact()
    {
        isBeingPushed = false;
        rb.velocity = Vector2.zero; // Reset the velocity after the push effect ends
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            Debug.Log("Player dies");

            Destroy(gameObject);
        }
    }
}
