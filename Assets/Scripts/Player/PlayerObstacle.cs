using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerObstacle : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    BoxCollider2D hitBox;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Die"))
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
            Die();
        }
    }
    private void Die()
    {
        PlayerStatics.playerHitState = PlayerStatics.PlayerHitState.death;
        animator.SetInteger("PlayerHit", (int)PlayerStatics.playerHitState);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerStatics.ResetDefalut();
    }

    private void SwitchToIdleAnimation()
    {
        // PlayerStatics.playerMovementSpeed = 0;
        PlayerStatics.movementState = PlayerStatics.PlayerMovementState.idle;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Stop"))
    //     {
    //         CheckStopObstacleHit();
    //         PlayerStatics.isPlayerStoped = true;
    //         // PlayerStatics.playerMovementSpeed = 0;
    //         Invoke("ReloadLevel", 3f);
    //     }
    // }

    // private void CheckStopObstacleHit()
    // {
    //     PlayerStatics.playerHitState = PlayerStatics.PlayerHitState.hurt;
    //     animator.SetInteger("PlayerHit", (int)PlayerStatics.playerHitState);
    //     // PlayerStatics.playerMovementSpeed = 0;
    //     PlayerStatics.movementState = PlayerStatics.PlayerMovementState.idle;
    //     Rigidbody2D player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    //     float divideBy = player.velocity.x / 2;
    //     player.velocity = new Vector2(-divideBy, 0);

    // }
}
