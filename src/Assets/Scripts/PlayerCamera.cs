using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;

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
        transform.position = player.position + offset;
    }
}
