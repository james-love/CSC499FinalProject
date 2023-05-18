using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager.Instance.AdjustHealth(-1);
            other.GetComponent<PlayerController>().ResetPos();
        }
    }
}
