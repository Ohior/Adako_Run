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

    private bool wasTriggered = false;

    private enum PlayerMovementState { run, jump, punch, roll }
    PlayerMovementState movementState = PlayerMovementState.jump;
    private enum TriggerTypeState { run, jump, punch, roll, none }
    private TriggerTypeState triggerType = TriggerTypeState.none;


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
        if (Input.GetButtonDown("Jump") && IsGrounded() && triggerType == TriggerTypeState.none)
        {
            Jump();
        }
        UpdateAnimationState();
        Debug.DrawRay(transform.position, Vector3.down * 2f, Color.red);
    }

    private void UpdateAnimationState()
    {
        if (triggerType == TriggerTypeState.none)
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
        else if (triggerType == TriggerTypeState.jump)
        {
            if (IsGrounded())
            {
                wasTriggered = false;
                moveSpeed = Constants.MoveSpeed;
                jumpForce = Constants.JumpForce;
                movementState = PlayerMovementState.run;
                triggerType = TriggerTypeState.none;
            }

        }
        if (Input.GetButtonDown("Jump"))
        {
            wasTriggered = true;
            switch (triggerType)
            {
                case TriggerTypeState.roll:
                    movementState = PlayerMovementState.roll;
                    collider2d.radius = Constants.PlayerColliderRadius / 2f;
                    break;
                case TriggerTypeState.jump:
                    Jump();
                    movementState = PlayerMovementState.jump;
                    break;
                default:
                    break;
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
        // bool grounded = Physics2D.BoxCast(
        //     collider2d.bounds.center,
        //     collider2d.bounds.size,
        //     0f,
        //     Vector2.down,
        //     0.1f,
        //     mask ?? layerMask
        // );
        // if (!shouldStartRunning) shouldStartRunning = grounded;
        // return grounded;

        // Calculate the position of the ray's origin at the bottom of the circle collider.
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - collider2d.radius);
        // Cast a ray from the rayOrigin position downward.
        bool grounded = Physics2D.Raycast(rayOrigin, Vector2.down, 1f, layerMask);
        if (!shouldStartRunning) shouldStartRunning = grounded;
        // Debug draw the ray for visualization (optional).
        Debug.DrawRay(rayOrigin, Vector2.down * 1f, Color.red);
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
        switch (other.gameObject.name)
        {
            case "JumpIcon":
                triggerType = TriggerTypeState.jump;
                break;
            case "RollPlatform":
                jumpForce = 0f;
                triggerType = TriggerTypeState.roll;
                break;
            default:
                triggerType = TriggerTypeState.none;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!wasTriggered)
        {
            triggerType = TriggerTypeState.none;
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
        triggerType = TriggerTypeState.none;
        collider2d.radius = Constants.PlayerColliderRadius;
        wasTriggered = false;
    }
}
