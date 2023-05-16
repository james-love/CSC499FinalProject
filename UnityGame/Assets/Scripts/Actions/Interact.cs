using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Interact : PlayerAction
{
    [SerializeField] private LayerMask interactMask;

    protected override void OnActionStarted(CallbackContext context)
    {
        Collider[] hit = Physics.OverlapBox(transform.position, new Vector3(0.5f, 1f, 0.5f), Quaternion.identity, interactMask, QueryTriggerInteraction.Collide);
        if (hit != null && hit.Length != 0)
        {
            hit[0].gameObject.GetComponent<Interactable>().Interact();
        }
    }
}
