using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Interact : MonoBehaviour
{
    [SerializeField] private LayerMask interactMask;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions.FindAction("Player/Interact").started += InteractPressed;
    }

    private void InteractPressed(CallbackContext context)
    {
        if (context.started)
        {
            Collider[] hit = Physics.OverlapBox(transform.position, new Vector3(1f, 2f, 1f), Quaternion.identity, interactMask);
            if (hit != null && hit.Length != 0)
            {
                hit[0].gameObject.GetComponent<Interactable>().Interact();
            }
        }
    }
}
