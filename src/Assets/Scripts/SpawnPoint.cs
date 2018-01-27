using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool isEmpty = true;

    [SerializeField]
    private Vector3 spawnRotation = Vector3.zero;

    public bool IsEmpty
    {
        get
        {
            return isEmpty;
        }
        set
        {
            isEmpty = value;
        }
    }
}
