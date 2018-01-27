using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevelIndex = int.MaxValue;

    public const string levelOneSceneName = "Level 1";
    public const string levelTwoSceneName = "Level 2";

    public const Direction levelOneStartDirection = Direction.Right;
    public const Direction levelTwoStartDirection = Direction.Right;

    [System.Serializable]
    public struct levelInfo
    {
        public Direction dir;
        public string lvlName;
    };
    public levelInfo[] m_levelinfo = new levelInfo[2];

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

    private levelInfo GetCurrentLevel()
    {
        return m_levelinfo[currentLevelIndex];
    }
}
