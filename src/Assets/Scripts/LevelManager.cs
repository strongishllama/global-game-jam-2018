using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct levelInfo
    {
        public Direction dir;
        public string lvlName;
    };

    private int currentLevelIndex = int.MaxValue;

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

    public levelInfo GetCurrentLevel()
    {
        return m_levelinfo[currentLevelIndex];
    }
}
