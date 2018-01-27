using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;

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
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.R))
        {
            Reset();
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Escape))
        {
            ForceQuit();
        }

        if (Input.GetKeyDown(KeyCode.P)) 
        {
            var cars = GameObject.FindObjectsOfType<CarDrift>();
            if (cars.Length <= 4) {
                bool hasLocalPlayer = false;
                for (int i = 0; i < cars.Length; i++) {
                    if (!cars[i].m_IsNetworkedCar) {
                        hasLocalPlayer = true;
                        break;
                    }
                }
                if (!hasLocalPlayer) {
                    NetworkReceiver nr = FindObjectOfType<NetworkReceiver>();
                    GameObject NewCar = Instantiate(nr.CarInstance, nr.GetSpawnPoint(), nr.CarInstance.transform.rotation);
                    //nr.players.Add(NewCar.GetComponent<CarDrift>());
                }
            }
        }
    }

    private void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ForceQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
