using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    public Transform groundCheck;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer = ~0;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool jumpPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 move = new Vector2(moveInput.x, 0f);
        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);

        // Jump
        if (jumpPressed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        jumpPressed = false; // reset after use
    }

    public void OnMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            jumpPressed = true;
    }

    private bool IsGrounded()
    {
        Vector2 origin;
        float distance = groundCheckDistance;

        if (groundCheck != null)
        {
            origin = groundCheck.position;
        }
        else
        {
            origin = (Vector2)transform.position;
        }

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, distance, groundLayer);
        return hit.collider != null;
    }
}