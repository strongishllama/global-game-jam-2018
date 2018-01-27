using UnityEngine;

public class PortalTestMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10.0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
    }
}
