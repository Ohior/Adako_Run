using Assets.Game_Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private CircleCollider2D boxCollider;
    private Animator animator;
    [SerializeField] private LayerMask layerMask;

    private float jumpForce = 7f;
    private float moveSpeed = 5f;

    private bool punchAction = false;
    private bool shouldStartRunning = false;

    private enum PlayerMovementState { run, jump, punch }
    PlayerMovementState movementState = PlayerMovementState.jump;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldStartRunning) rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (IsGrounded())
        {
            movementState = PlayerMovementState.run;
        }
        else if (!IsGrounded())
        {
            movementState = PlayerMovementState.jump;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (punchAction)
            {
                movementState = PlayerMovementState.punch;
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
            boxCollider.bounds.center,
            boxCollider.bounds.size,
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
        punchAction = true;
        jumpForce = 0f;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        punchAction = false;
        jumpForce = 7f;
    }

    private void Die()
    {
        animator.SetTrigger("Death");
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
