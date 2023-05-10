using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Attack : MonoBehaviour
{
    [SerializeField] private Sword sword;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions.FindAction("Player/DrawSword").started += DrawSwordPressed;
        playerInput.actions.FindAction("Player/Attack").started += AttackPressed;
    }

    private void DrawSwordPressed(CallbackContext context)
    {
        if (context.started)
        {
            print("draw sword pressed");
        }
    }

    private void AttackPressed(CallbackContext context)
    {
        if (context.started)
        {
            print("attack pressed");
        }
    }
}
