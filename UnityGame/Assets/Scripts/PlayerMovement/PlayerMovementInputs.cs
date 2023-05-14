using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInputs : MonoBehaviour
{
    [SerializeField] private bool cursorInputForLook = true;
    [field: SerializeField] public bool CursorLocked { get; set; } = true;
    [field: SerializeField] public bool AlwaysRun { get; private set; } = false;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Jump { get; set; }
    public bool Sprint { get; private set; }

    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        Move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        Look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        Jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        Sprint = newSprintState;
    }

    public void SetAlwaysRun(bool newValue)
    {
        AlwaysRun = newValue;
        PlayerPrefs.SetInt("AlwaysRun", AlwaysRun ? 1 : 0);
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("AlwaysRun"))
            AlwaysRun = PlayerPrefs.GetInt("AlwaysRun") == 1;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(CursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
