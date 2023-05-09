using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject handTarget;
    [SerializeField] private GameObject hipTarget;
    [SerializeField] private GameObject offsetPoint;

    [SerializeField] private bool inHand = false;

    private Vector3 inHandPosOffset = new(-0.06f, 0.09f, 0.01f);
    private Vector3 inHandRotOffset = new(-90f, 90f, 0f);

    private Vector3 onHipPosOffset = new(-0.18f, 0f, 0f);
    private Vector3 onHipRotOffset = new(-160f, 0f, 10f);

    private void Update()
    {
        if (inHand)
        {
            transform.SetPositionAndRotation(handTarget.transform.position, handTarget.transform.rotation);
            offsetPoint.transform.localPosition = inHandPosOffset;
            offsetPoint.transform.localRotation = Quaternion.Euler(inHandRotOffset);
        }
        else
        {
            transform.SetPositionAndRotation(hipTarget.transform.position, hipTarget.transform.rotation);
            offsetPoint.transform.localPosition = onHipPosOffset;
            offsetPoint.transform.localRotation = Quaternion.Euler(onHipRotOffset);
        }
    }
}
