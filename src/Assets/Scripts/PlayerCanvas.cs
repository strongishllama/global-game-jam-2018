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
    }

    private void Update()
    {
        if (GameManager.instance.GameIsRunning)
        {
            UpdateTimeDisplay();
            UpdateLapDisplay();
        }
    }

    private void UpdateTimeDisplay()
    {
        timeElapsed += (decimal)Time.deltaTime;
        timeDisplay.text = "Time: " + Math.Round(timeElapsed, 1, MidpointRounding.ToEven);
    }

    private void UpdateLapDisplay()
    {
        lapDisplay.text = "Lap: " + currentLapCount + "/" + maxLapCount;
    }

    public void IncrementLapCount()
    {
        ++currentLapCount;
        UpdateLapDisplay();
    }
}
