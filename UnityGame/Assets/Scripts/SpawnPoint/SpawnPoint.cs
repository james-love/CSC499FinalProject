using UnityEngine;

public class SpawnPoint : MonoBehaviour, ISpawnPoint
{
    [field: SerializeField] public int SpawnPointIndex { get; set; }
    [field: SerializeField] public bool HasRotation { get; set; }
    [field: SerializeField] public float Rotation { get; set; }

    private void OnDrawGizmosSelected()
    {
        var rotatedPoint = transform.position + (Quaternion.AngleAxis(Rotation, Vector3.up) * Vector3.forward);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, rotatedPoint);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(rotatedPoint, 0.1f);
    }
}
