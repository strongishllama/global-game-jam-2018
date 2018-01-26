using UnityEngine;

public class PortalEntrance : MonoBehaviour
{
    [SerializeField]
    private Transform exit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = exit.position;
        }
    }
}
