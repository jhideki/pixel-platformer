using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float horizontalSpeed = 5f;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(PlayerManager.instance.GetCurrentState() == PlayerManager.PlayerState.companion)
        {
            Movement();
        }
    }

    void Movement()
    {
        // Check for jump input
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        // Update horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
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
}
