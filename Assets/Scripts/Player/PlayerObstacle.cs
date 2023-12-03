using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerObstacle : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private Animator animator;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        // PlayerStatics.playerMovementSpeed = -PlayerStatics.playerMovementSpeed;
        PlayerStatics.movementState = PlayerStatics.PlayerMovementState.idle;
    }
}
