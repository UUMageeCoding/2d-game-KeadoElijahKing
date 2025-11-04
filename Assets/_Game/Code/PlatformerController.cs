using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.InputSystem.OSX;


public class PlatformerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;
    float horizontalMovement; 
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallGravityMult = 2f; 


    [Header("Wall Check")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius = 0.2f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);

    [Header("WallMovement")]
    public float wallSlideSpeed = 2f;
    //Wall Jumping
    bool isWallJumping;
    float wallJumpDirection;
    float wallJumpTime = 0.5f;
    float wallJumpTimer;
    public Vector2 wallJumpPower = new Vector2(5f, 10f);
    bool isWallSliding;
    
    private Rigidbody2D rb;
    
    private bool isWall;

    private float moveInput;
    private bool isMovingRight;
    private bool isJumping;
 
    private SpriteRenderer sr;

    private Vector3 respawnPoint;
    public GameObject FallDetector;
    public Animator anim; 
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
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


        isWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
        Debug.Log("isWall " + isWall);
        if (isWallJumping)
        {
            isWallJumping = true;
        }


        FallDetector.transform.position = new Vector2(transform.position.x, FallDetector.transform.position.y);

        GroundCheck();
        ProcessGravity();
        ProcessWallSlide();
        ProcessWallJump();

        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalMovement * moveSpeed, rb.linearVelocity.y);
            Flip();
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
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("linear velocity " + rb.linearVelocity);
            anim.SetBool("isJumping", true);
            isGrounded = true;

        }
        else
        {
            anim.SetBool("isJumping", false);
            isGrounded = false;
        }

        //Wall Jump
        if (Input.GetButtonDown("Jump") && isWallJumping)
        {
           // isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpTimer = 0;

            //force flip
            if (transform.localScale.x != wallJumpDirection)
            {
                isMovingRight = !isMovingRight;
            Vector3 Ls = transform.localScale;
            Ls.x *= -1f;
            transform.localScale = Ls;
            }

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f);
        }
    }
    private bool WallCheck()
    {
        return Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
    }



    private void ProcessGravity()
    {
        //falling gravity
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = baseGravity * fallGravityMult; // fall faster and faster
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void ProcessWallSlide()
    {
        if (!isGrounded & WallCheck() & horizontalMovement != 0)
        {
            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, MathF.Max(rb.linearVelocity.y, -wallSlideSpeed));
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void ProcessWallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpTimer = wallJumpTime;

            CancelInvoke(nameof(CancelWallJump));
        }
        else if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }
        
    }
    private void CancelWallJump()
    {
        isWallJumping = false; 
    }
    private void Flip()
    {
        if (isMovingRight && horizontalMovement < 0 || !isMovingRight && horizontalMovement > 0)
        {
            isMovingRight = !isMovingRight;
            Vector3 Ls = transform.localScale;
            Ls.x *= -1f;
            transform.localScale = Ls;
        }
    }
}