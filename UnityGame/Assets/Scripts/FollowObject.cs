using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private GameObject followTarget;
    [SerializeField] private bool matchPosition = true;
    [SerializeField] private bool matchRotation = true;

    private void Update()
    {
        if (matchPosition)
            transform.position = followTarget.transform.position;
        if (matchRotation)
            transform.rotation = followTarget.transform.rotation;
    }
}
