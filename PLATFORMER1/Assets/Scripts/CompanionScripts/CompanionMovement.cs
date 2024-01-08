using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float horizontalSpeed = 5f;
    public bool isFacingRight;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private bool isJumping = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        if(PlayerManager.instance.GetCurrentState() == PlayerManager.PlayerState.companion)
        {
            Movement(horizontalInput);
            rb.simulated = true;
        }

        FlipSprite(horizontalInput);
    }

    void FixedUpdate()
    {
        if(isJumping)
        {
            Jump();
        }
    }

    void Movement(float horizontalInput)
    {
        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if(Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        // Update horizontal movement
        MoveHorizontal(horizontalInput);
    }

    void Jump()
    {
        // Apply upward force for jumping
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void MoveHorizontal(float horizontalInput)
    {
        // Move the object horizontally based on the input
        Vector2 moveVelocity = new Vector2(horizontalInput * horizontalSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;

    } 

    void FlipSprite(float horizontalInput)
    {
        // Flip the sprite based on the direction
        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = false; // facing left 
            isFacingRight = false;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = true; // facing right 
            isFacingRight = true;
        }
    }
}
