using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    private float inputVertical;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private bool isMovingRight;
    public float distance;

    private SpriteRenderer sr;

    private Vector3 respawnPoint;
    public GameObject FallDetector;
    
     public LayerMask whatIsLadder;
    private bool isClimbing;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
        // Set to Dynamic with gravity
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 3f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        isMovingRight = true;

        respawnPoint = transform.position;
         rb = GetComponent<Rigidbody2D>();
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
        }
        else
        {
            sr.flipX = true;
        }
            // Check if grounded
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("linear velocity " + rb.linearVelocity); 
        }

        FallDetector.transform.position = new Vector2(transform.position.x, FallDetector.transform.position.y);
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
         RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isClimbing = true;
            }
        }
        if(isClimbing == true)
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector2(rb.position.x, inputVertical * moveSpeed);
            rb.gravityScale = 0;
        }
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
}