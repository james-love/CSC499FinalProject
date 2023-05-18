using UnityEngine;
using UnityEngine.Events;

public class BroadcastEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent<Collider> onTriggerEnter;
    [SerializeField] private UnityEvent<Collider> onTriggerStay;

    private void OnTriggerEnter(Collider col)
    {
        onTriggerEnter?.Invoke(col);
    }

    private void OnTriggerStay(Collider col)
    {
        onTriggerStay?.Invoke(col);
    }
}
