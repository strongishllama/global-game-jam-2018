using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField]
    private int maxLapCount = 3;
    private int currentLapCount = 1;

    private decimal timeElapsed = 0;

    [SerializeField]
    private Text timeDisplay;
    [SerializeField]
    private Text lapDisplay;
    [SerializeField]
    private Text winnerDisplay;

    public static PlayerCanvas instance;

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

        if (timeDisplay == null)
        {
            Debug.Log("Warning:" + gameObject.name + " does not have an time display assigned.");
        }

        if (lapDisplay == null)
        {
            Debug.Log("Warning:" + gameObject.name + " does not have an lap display assigned.");
        }

        if (winnerDisplay == null)
        {
            Debug.Log("Warning:" + gameObject.name + " does not have an winner display assigned.");
        }

       lapDisplay.text = "Lap: " + currentLapCount + "/" + maxLapCount;
    }

    private void Update()
    {
        if (GameManager.instance.GameIsRunning)
        {
            UpdateTimeDisplay();
        }
    }

    private void UpdateTimeDisplay()
    {
        timeElapsed += (decimal)Time.deltaTime;
        timeDisplay.text = "Time: " + Math.Round(timeElapsed, 1, MidpointRounding.ToEven);
    }

    public void IncrementLapCount()
    {
        ++currentLapCount;
        lapDisplay.text = "Lap: " + currentLapCount + "/" + maxLapCount;

        if (currentLapCount >= maxLapCount)
        {
            winnerDisplay.text = "You Win!";
            Time.timeScale = 0.0f;
        }
    }
}
