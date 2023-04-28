using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DisplayInteractPopup : MonoBehaviour
{
    private const float PopupExtentsY = 1f;
    private const float PopupOffset = 0.3f;
    private AsyncOperationHandle<GameObject> opHandle;
    private GameObject interactPopup;

    private void Reset()
    {
        this.hideFlags = HideFlags.HideInInspector;
    }

    private void Awake()
    {
        StartCoroutine(LoadAsset());
    }

    private void OnDestroy()
    {
        Addressables.Release(opHandle);
    }

    private IEnumerator LoadAsset()
    {
        opHandle = Addressables.LoadAssetAsync<GameObject>("InteractPopup");
        yield return opHandle;

        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Collider col = GetComponent<Collider>();
            interactPopup = Instantiate(opHandle.Result, new Vector3(col.bounds.center.x, col.bounds.center.y + col.bounds.extents.y + PopupExtentsY + PopupOffset, col.bounds.center.z), Quaternion.identity);
            interactPopup.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && interactPopup != null)
            interactPopup.SetActive(true);
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") && interactPopup != null)
            interactPopup.SetActive(false);
    }
}
