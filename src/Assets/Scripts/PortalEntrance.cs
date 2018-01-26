using UnityEngine;

public class PortalEntrance : MonoBehaviour
{
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
            other.transform.position = exit.position;
        }
    }
}
