using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushJump : MonoBehaviour
{
    private float yForce = PlayerStatics.playerJumpForce * 1.7f;
    private float xForce = PlayerStatics.playerMovementSpeed / 2f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xForce, yForce);
            animator.SetBool("activateTrampoline", true);
        }
    }
}
