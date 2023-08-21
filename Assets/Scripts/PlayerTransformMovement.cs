using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public int beatsPerMinute = 120;
    public float jumpForce = 15f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Rigidbody2D rb;

    private bool isGrounded;
    private bool isJumping;


    private void Start()
    {
        float levelSpeed = moveSpeed * beatsPerMinute / 120;
    }

    private void Update()
    {
        // Jump input
        if (isGrounded && Input.GetMouseButtonDown(0))
        {
            isJumping = true;
            Jump();
        }
    }

    private void FixedUpdate()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Move the player to the right
        transform.Translate(Vector2.right * moveSpeed * Time.fixedDeltaTime);

        if (isJumping)
        {
            Jump();
            isJumping = false;
        }


    }

    private void Jump()
    {
        // Apply an initial vertical force for jumping
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
