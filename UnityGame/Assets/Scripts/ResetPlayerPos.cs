using StarterAssets;
using UnityEngine;

public class ResetPlayerPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ThirdPersonController>().ResetPos();
        }
    }
}
