using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public const string levelOneSceneName = "Level 1";

    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            Debug.Log(gameObject + " singleton instance was destroyed because a second one was created.");
        }
    }
}
