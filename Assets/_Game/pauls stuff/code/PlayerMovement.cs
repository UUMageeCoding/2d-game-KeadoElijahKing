using System;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private float wallSlideCoolDown;

    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 12f);
    private Rigidbody2D rb;

    private float moveInput;
    private bool isMovingRight;
    private bool isJumping;
 
    private SpriteRenderer sr;

    private Vector3 respawnPoint;
    public GameObject FallDetector;
    public Animator anim;
    private BoxCollider2D boxCollider;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        // Set to Dynamic with gravity
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        isMovingRight = true;
        anim = GetComponent<Animator>();

        respawnPoint = transform.position;
    }

    void Update()
    {
        // Get horizontal input
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0) isMovingRight = true;
        else isMovingRight = false;

        if (isMovingRight)
        {
            sr.flipX = false;
            anim.SetBool("isRunning", true);
        }
        else
        {
            sr.flipX = true;
            anim.SetBool("isRunning", true);
        }
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }


        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input
        
        if (wallSlideCoolDown < 0.2f)
        {
            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

                anim.SetBool("isJumping", true);


            }
            else
            {
                anim.SetBool("isJumping", false);
            }
        if (onWall() && !isGrounded ())
            {
                rb.gravityScale = 0;
                rb.linearVelocity = Vector2.zero;
                if (wallJumpTimer > 0f)
                {
                    isWallJumping = true;
                    rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y); 
                }
            }
     }


        FallDetector.transform.position = new Vector2(transform.position.x, FallDetector.transform.position.y);
    }
private void wallSlide() 
    {
        if (onWall() && !isGrounded())
        {
            wallSlideCoolDown = 0;
            rb.linearVelocity = new Vector2(-MathF.Sign(transform.localScale.x) * 3, 6);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FallDetector"))
        {
            transform.position = respawnPoint;
            rb.linearVelocity = Vector2.zero; // Reset velocity upon respawn
        }
    }
    void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

    }
    // Visualise ground check in editor
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
