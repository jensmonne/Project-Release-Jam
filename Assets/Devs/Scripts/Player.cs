using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckDistance = 0.2f;
    [SerializeField] LayerMask groundLayer = ~0;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    [SerializeField] private GameObject CharachterSelectCursor;
    private GameObject CharachterMoveCursor;

    public bool hasItem = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CharachterMoveCursor = Instantiate(CharachterSelectCursor, Vector2.zero, Quaternion.identity, transform);

    }

    public void SpawnCursor(Color color)
    {
        CharachterMoveCursor = Instantiate(CharachterSelectCursor, Vector2.zero, Quaternion.identity, transform);
        CharachterMoveCursor.GetComponent<SpriteRenderer>().color = color;
    }

    private void SelectCharachter()
    {
        if (CharachterMoveCursor.GetComponent<CircleCollider2D>().IsTouchingLayers(LayerMask.GetMask("Character")))
        {
            Debug.Log("Selected Character!");
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isRoundActive)
        {
            Vector2 move = new Vector2(moveInput.x, 0f);
            rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
        }

        if (!GameManager.Instance.isRoundActive)
        {
            Vector2 move = new Vector2(moveInput.x, moveInput.y);
            CharachterMoveCursor.transform.Translate(move * (speed * 1.5f) * Time.fixedDeltaTime);
        }


        if (jumpPressed && IsGrounded() && GameManager.Instance.isRoundActive)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else if (!GameManager.Instance.isRoundActive && jumpPressed)
        {
            SelectCharachter();
        }

        jumpPressed = false;
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


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun") && !hasItem)
        {
            Debug.Log("Player picked up a gun!");

            Destroy(other.gameObject);

            Gun_Aimer gunAimer = gameObject.GetComponent<Gun_Aimer>();

            gunAimer.EnableGun();
        }

        if (other.CompareTag("Hamer") && !hasItem)
        {
            Debug.Log("Player picked up a hammer!");

            Destroy(other.gameObject);

            Hamer_Swinger hammerSwinger = gameObject.GetComponent<Hamer_Swinger>();

            hammerSwinger.EnableHammer();
        }
    }
}