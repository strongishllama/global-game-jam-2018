using UnityEngine;

public enum eGameState
{
    PreGame,
    PlayingGame,
    PostGame
}

public class GameManager : MonoBehaviour
{
    private eGameState gameState = eGameState.PreGame;

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

    private void Update()
    {
        
    }

    public eGameState GameState
    {
        get
        {
            return gameState;
        }
        set
        {
            gameState = value;
        }
    }
}
