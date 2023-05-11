using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Attack : MonoBehaviour
{
    [SerializeField] private Sword sword;
    private PlayerInput playerInput;
    private Animator playerAnimator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponentInChildren<Animator>();

        playerInput.actions.FindAction("Player/DrawSword").started += DrawSwordPressed;
        playerInput.actions.FindAction("Player/LightAttack").performed += AttackPressed;
        playerInput.actions.FindAction("Player/HeavyAttack").performed += HeavyAttackPressed;
    }

    private void DrawSwordPressed(CallbackContext context)
    {
        if (context.started)
        {
            sword.ToggleSword(playerAnimator);
        }
    }

    private void AttackPressed(CallbackContext context)
    {
        if (context.performed)
        {
            sword.LightAttack(playerAnimator);
        }
    }

    private void HeavyAttackPressed(CallbackContext context)
    {
        if (context.performed)
        {
            sword.HeavyAttack(playerAnimator);
        }
    }
}
