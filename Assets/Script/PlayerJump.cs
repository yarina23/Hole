using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 8f;        // Initial punch off the ground
    public float holdBoost = 12f;       // Extra lift while holding Space
    public float maxHoldTime = 0.8f;    // Maximum hold duration

    private Rigidbody rb;
    private float holdTimer = 0f;
    private bool isJumping = false;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // 1. First frame Space is pressed -> Instant jump impulse
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            isGrounded = false;
            holdTimer = 0f;
        }

        // 2. Track hold time up to 0.8 seconds
        if (Keyboard.current.spaceKey.isPressed && isJumping)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= maxHoldTime)
            {
                isJumping = false; // Cut off boost after 0.8s
            }
        }

        // 3. Releasing Space cuts the boost short immediately
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            isJumping = false;
        }
    }

    void FixedUpdate()
    {
        // Apply extra upward thrust while holding space within the 0.8s window
        if (isJumping && Keyboard.current.spaceKey.isPressed)
        {
            rb.AddForce(Vector3.up * holdBoost, ForceMode.Force);
        }
    }

    // Automatically detects when the ball lands back on the platform
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        isJumping = false;
    }
}