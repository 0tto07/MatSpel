using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public FatassData data;
    //public StatApplier statApplier;
    public float moveSpeed = 5f;
    public Camera mainCamera; // Reference to the main camera

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //moveSpeed = 5f + statApplier.speedMod;
    }

    void Update()
    {
        //rb.drag = 0.1f + statApplier.weightMod;
        // Input handling for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalizing the vector to maintain consistent speed
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        

    }

    void FixedUpdate()
    {
        // Applying movement to the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }  
}