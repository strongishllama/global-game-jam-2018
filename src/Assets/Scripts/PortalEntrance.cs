using System.Collections;
using UnityEngine;

public class PortalEntrance : MonoBehaviour
{
    [SerializeField]
    private float portalTransferDelay = 0.3f;
    [SerializeField]
    private float portalCooldown = 2.0f;

    [SerializeField]
    private GameObject portalEntranceEffect;
    [SerializeField]
    private GameObject portalExitEffect;

    [SerializeField]
    private Transform exit;

    private void Awake()
    {
        if (exit == null)
        {
            Debug.Log("Warning:" + gameObject.name + " does not have an exit portal assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && exit != null)
        {
            StartCoroutine(ActivatePortal(other));
        }
    }

    private IEnumerator ActivatePortal(Collider other)
    {
        portalEntranceEffect.SetActive(true);
        other.gameObject.SetActive(false);
        yield return new WaitForSeconds(portalTransferDelay);

        other.transform.position = exit.position;
        portalExitEffect.SetActive(true);
        other.gameObject.SetActive(true);
        yield return new WaitForSeconds(portalCooldown);

        portalEntranceEffect.SetActive(false);
        portalExitEffect.SetActive(false);
    }
}
