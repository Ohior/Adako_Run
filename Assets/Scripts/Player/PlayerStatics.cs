public static class PlayerStatics
{
    public static float playerMovementSpeed = 5f;
    public static float playerJumpForce = 8f;
    public static bool isPlayerStoped = false;
    public static PlayerMovementState movementState = PlayerMovementState.jump;
    public enum PlayerMovementState { run, jump, punch, roll, idle, none }

    public enum PlayerActionState { run, jump, punch, roll, idle, none }
    public static PlayerActionState actionState = PlayerActionState.none;

    public static PlayerHitState playerHitState = PlayerHitState.none;
    public enum PlayerHitState { none, death, hurt }

    public static bool activateGrounded = true;

    public static void ResetDefalut()
    {
        playerJumpForce = 8f;
        playerMovementSpeed = 5f;
        isPlayerStoped = false;
        movementState = PlayerMovementState.jump;
        actionState = PlayerActionState.none;
        playerHitState = PlayerHitState.none;
        activateGrounded = true;
    }
}