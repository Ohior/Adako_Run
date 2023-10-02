using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushJump : MonoBehaviour
{
    [SerializeField] private float yForce = 5f;
    [SerializeField] private float xForce = 0f;
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
