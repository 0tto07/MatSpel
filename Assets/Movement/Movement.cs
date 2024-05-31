using UnityEngine;

public class Movement : MonoBehaviour
{
    public StatApplier statApplier;
    public float moveSpeed;
    public Camera mainCamera;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isBeingPushed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 1f + (statApplier != null ? statApplier.speedMod : 0);
    }

    void Update()
    {
        rb.drag = 0.05f + (statApplier != null ? statApplier.weightMod : 0);

        if (!isBeingPushed)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement.magnitude > 1)
            {
                movement.Normalize();
            }
        }
    }

    void FixedUpdate()
    {
        if (!isBeingPushed)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void StartPushImpact(float duration, Vector2 pushForce)
    {
        isBeingPushed = true;
        rb.AddForce(pushForce, ForceMode2D.Impulse);

        Invoke(nameof(EndPushImpact), duration);
    }

    private void EndPushImpact()
    {
        isBeingPushed = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Arena"))
        {
            Debug.Log("Player exits the arena and dies.");
            Destroy(gameObject);
        }
    }
}
