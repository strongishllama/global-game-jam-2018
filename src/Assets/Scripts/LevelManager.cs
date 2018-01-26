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
            Debug.Log(gameObject.name + " singleton instance was destroyed because a second one was created.");
            Destroy(gameObject);
        }
    }
}
