public static class PlayerStatics
{
    public static float playerMovementSpeed = 10f;
    public static float playerJumpForce = 10f;
    public static bool isPlayerStoped = false;
    public static bool isComingDown = true;
    public enum PlayerMovementState { run, jump, punch, roll, idle }

    public static PlayerMovementState movementState = PlayerMovementState.jump;

    public static void ResetDefalut()
    {
        playerJumpForce = 10f;
        playerMovementSpeed = 10f;
        isPlayerStoped = false;
        movementState = PlayerMovementState.jump;
        isComingDown = true;
    }
}