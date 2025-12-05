using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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

    public float moveInput;
    public bool isMovingRight;
    private bool isJumping;
 
    public SpriteRenderer sr;

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