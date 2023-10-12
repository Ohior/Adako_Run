using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Game_Assets.Scripts;
using UnityEngine.SceneManagement;

public class PlayerObstacle : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Die"))
        {
            rigidbody.bodyType = RigidbodyType2D.Static;
            Die();
        }
        else if (other.gameObject.CompareTag("Stop"))
        {
            PlayerStatics.isPlayerStoped = true;
            PlayerStatics.playerMovementSpeed = 0;
            PlayerStatics.movementState = PlayerStatics.PlayerMovementState.idle;
            Invoke("ReloadLevel", 3f);
        }
    }
    private void Die()
    {
        animator.SetTrigger("Death");
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerStatics.ResetDefalut();
    }
}
