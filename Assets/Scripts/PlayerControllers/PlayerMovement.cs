using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float doubleJumpForce = 5f;
    public int maxJumps = 1; // Set to 1 for no double jump

    [Header("Movement Settings")]
    public Animator animator;



    [Header("Ground Detection")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private float moveInputHorizontal;
    private bool isGrounded;
    private int jumpCount;

    private PlayerController inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initialize Input Actions
        inputActions = new PlayerController();

        // Subscribe to input events
        inputActions.Land.Jump.performed += OnJump;
        inputActions.Land.Move.performed += ctx => moveInputHorizontal = ctx.ReadValue<float>();
        inputActions.Land.Move.canceled += ctx => moveInputHorizontal = 0f;
    }

    private void OnEnable()
    {
        inputActions.Land.Enable();
    }

    private void OnDisable()
    {
        inputActions.Land.Disable();
    }

    private void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Reset jumps if grounded
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    private void FixedUpdate()
    {
  
        Debug.Log(rb.velocity.x);
        rb.velocity = new Vector2(moveInputHorizontal * moveSpeed, rb.velocity.y);
        
        if (moveInputHorizontal != 0)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                animator.Play("StartToRun");
            }
            else
            {
                animator.Play("Run");
            }
        }
        else
        {
            animator.Play("Idle");
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded || jumpCount < maxJumps)
        {
            animator.Play("Jump");
            float force = jumpCount == 0 ? jumpForce : doubleJumpForce;
            rb.velocity = new Vector2(rb.velocity.x, force);
            
            jumpCount++;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize ground check
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
