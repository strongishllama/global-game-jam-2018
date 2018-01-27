using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float dampSmoothTime = 0.5f;

    [SerializeField]
    private Vector3 offset;
    private Vector3 velocity;

    [SerializeField]
    private Transform player;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Debug.Log("Warning: " + gameObject.name + "'s player transform was not assigned to follow.");

            if (player == null)
            {
                Debug.Log("Warning: " + gameObject.name + " could not find a GameObject tagged as player to follow.");
            }
        }
    }

    private void Update()
    {
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, dampSmoothTime);
    }
}
