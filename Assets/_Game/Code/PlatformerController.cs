using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.InputSystem.OSX;


public class PlatformerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 12f;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private bool isGrounded;

    private float moveInput;
    private bool isMovingRight;
    private bool isJumping;
 
    private SpriteRenderer sr;

    private Vector3 respawnPoint;
    public GameObject FallDetector;
    public Animator anim; 

    private CapsuleCollider2D boxCollider;
    
    
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

        if (moveInput > 0)
        {
            isMovingRight = true;
            sr.flipX = false;
        } 
        else if (moveInput < 0)
        {
            isMovingRight = false;
            sr.flipX = true;    
        }

        if (moveInput > 0 || moveInput < 0)
        {
            //sr.flipX = false;
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }


        


        // else
        // {
        //     //sr.flipX = true;
        //     anim.SetBool("isRunning", true);
        // }
        // if (moveInput == 0)
        // {
        //     anim.SetBool("isRunning", false);
        // }


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            Debug.Log("linear velocity " + rb.linearVelocity);
            anim.SetBool("isJumping", true);


        }
        else
        {
            anim.SetBool("isJumping", false);
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

    public bool canAttack()
    {
        return moveInput == 0 && isGrounded;
    }
    
}