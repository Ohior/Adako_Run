using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateJump : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
                collision.gameObject.GetComponent<Rigidbody2D>().velocity.x,
                jumpForce
            );
        }
    }
}
