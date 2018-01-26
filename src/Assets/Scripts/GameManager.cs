using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameIsRunning = false;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log(gameObject.name + " singleton instance was destroyed because a second one was created.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameIsRunning = true;
    }

    public bool GameIsRunning
    {
        get
        {
            return gameIsRunning;
        }
        set
        {
            gameIsRunning = value;
        }
    }
}
