using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;
    private Animator animator;
    [SerializeField] private LayerMask layerMask;

    private float jumpForce = 7f;
    private float moveSpeed = 3f;
    private bool shouldStartRunning = false;
    private PlayerMovementState movementState = PlayerMovementState.jump;

    private enum PlayerMovementState
    {
        run,jump
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldStartRunning) rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rigidbody.velocity = new Vector2(0f, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (!IsGrounded())
        {
            movementState = PlayerMovementState.jump;
        }
        else
        {
            movementState = PlayerMovementState.run;
        }
        animator.SetInteger("PlayerState", (int)movementState);
    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            layerMask
        );
        shouldStartRunning = grounded;
        return grounded;
    }
}
