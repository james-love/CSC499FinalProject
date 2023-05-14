using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(PlayerInput))]
public abstract class PlayerAction : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private InputActionReference action;
    protected virtual void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions.FindAction(action.name).started += OnActionStarted;
        playerInput.actions.FindAction(action.name).performed += OnActionPerformed;
        playerInput.actions.FindAction(action.name).canceled += OnActionCanceled;
    }

    protected virtual void OnActionStarted(CallbackContext context)
    {
        // Do nothing if not implemented.
    }

    protected virtual void OnActionPerformed(CallbackContext context)
    {
        // Do nothing if not implemented.
    }

    protected virtual void OnActionCanceled(CallbackContext context)
    {
        // Do nothing if not implemented.
    }
}
