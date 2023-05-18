using UnityEngine;
using UnityEngine.Events;

public class BroadcastEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider> onTriggerEnter;

    private void OnTriggerEnter(Collider col)
    {
        onTriggerEnter?.Invoke(col);
    }
}
