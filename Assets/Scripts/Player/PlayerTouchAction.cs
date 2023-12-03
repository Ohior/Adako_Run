using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchAction : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    private Rigidbody2D playerRB;
    private CapsuleCollider2D playerColObj;

    private string tagName = "None";
    private void Start()
    {
        playerRB = playerGameObject.GetComponent<Rigidbody2D>();
        playerColObj = playerGameObject.GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (tagName == "Roll" && Input.GetButtonDown("Jump"))
        {
            PlayerStatics.activateGrounded = false;
            PlayerStatics.movementState = PlayerStatics.PlayerMovementState.roll;
            PlayerStatics.actionState = PlayerStatics.PlayerActionState.roll;
            playerColObj.offset = new Vector2(0, -0.5f);
            playerColObj.size = new Vector2(0.7f, 1f);
        }
        else if (tagName == "Attack" && Input.GetButtonDown("Jump"))
        {
            PlayerStatics.activateGrounded = false;
            PlayerStatics.movementState = PlayerStatics.PlayerMovementState.punch;
            PlayerStatics.actionState = PlayerStatics.PlayerActionState.punch;
        }
        playerRB.velocity = new Vector2(PlayerStatics.playerMovementSpeed, playerRB.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        tagName = other.gameObject.tag;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tagName = "None";
        PlayerStatics.activateGrounded = true;
        // PlayerStatics.movementState = PlayerStatics.PlayerMovementState.roll;
        // PlayerStatics.actionState = PlayerStatics.PlayerActionState.roll;
        playerRB.velocity = new Vector2(PlayerStatics.playerMovementSpeed, playerRB.velocity.y);
        playerColObj.offset = new Vector2(0, 0f);
        playerColObj.size = new Vector2(1.3f, 1.8f);
    }
    private CollitionDataClass checkGrounded()
    {
        bool grounded = Physics2D.BoxCast(transform.position, playerColObj.bounds.size, 0f, Vector2.down, 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        return new CollitionDataClass(hit, hit.collider.tag);
    }
}
