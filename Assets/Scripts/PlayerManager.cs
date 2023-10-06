using Assets.Game_Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private CircleCollider2D collider2d;
    private Animator animator;
    [SerializeField] private LayerMask layerMask;

    private float jumpForce = Constants.JumpForce;
    private float moveSpeed = Constants.MoveSpeed;

    private bool shouldStartRunning = false;

    private bool isTriggerContact = false;
    private bool wasTriggered = false;

    private enum PlayerMovementState { run, jump, punch, roll }
    PlayerMovementState movementState = PlayerMovementState.jump;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldStartRunning) rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded() && !isTriggerContact)
        {
            Jump();
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (!isTriggerContact)
        {
            if (IsGrounded())
            {
                moveSpeed = Constants.MoveSpeed;
                jumpForce = Constants.JumpForce;
                movementState = PlayerMovementState.run;
            }
            else if (!IsGrounded())
            {
                moveSpeed = Constants.MoveSpeed - 5;
                movementState = PlayerMovementState.jump;
            }

        }
        if (Input.GetButtonDown("Jump"))
        {
            wasTriggered = true;
            if (isTriggerContact)
            {
                movementState = PlayerMovementState.roll;
                collider2d.radius = Constants.PlayerColliderRadius / 2f;
                // collider2d.offset = new Vector(c)
            }
        }

        animator.SetInteger("PlayerState", (int)movementState);
    }

    private void Jump(float force = 1)
    {
        rigidbody.velocity = new Vector2(moveSpeed, jumpForce + force);
    }

    private bool IsGrounded(LayerMask? mask = null)
    {
        bool grounded = Physics2D.BoxCast(
            collider2d.bounds.center,
            collider2d.bounds.size,
            0f,
            Vector2.down,
            0.1f,
            mask ?? layerMask
        );
        if (!shouldStartRunning) shouldStartRunning = grounded;
        return grounded;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Die"))
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        jumpForce = 0f;
        isTriggerContact = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!wasTriggered)
        {
            isTriggerContact = false;
        }
    }
    private void Die()
    {
        animator.SetTrigger("Death");
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResetPlayerAnimation()
    {
        isTriggerContact = false;
        collider2d.radius = Constants.PlayerColliderRadius;
    }
}
