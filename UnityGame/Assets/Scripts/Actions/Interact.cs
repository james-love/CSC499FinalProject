using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Interact : PlayerAction
{
    [SerializeField] private LayerMask interactMask;

    protected override void OnActionStarted(CallbackContext context)
    {
        Collider[] hit = Physics.OverlapBox(transform.position, new Vector3(1f, 2f, 1f), Quaternion.identity, interactMask);
        if (hit != null && hit.Length != 0)
        {
            hit[0].gameObject.GetComponent<Interactable>().Interact();
        }
    }
}
