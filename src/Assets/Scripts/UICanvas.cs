using System;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    [SerializeField]
    private int maxLapCount = 3;
    private int currentLapCount = 1;

    private decimal timeElapsed = 0;

    [SerializeField]
    private Text timeDisplay;
    [SerializeField]
    private Text winnerDisplay;

    public static UICanvas instance;

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
        if (GameManager.instance.GameState == eGameState.PlayingGame)
        {
            UpdateTimeDisplay();
        }
    }

    private void UpdateTimeDisplay()
    {
        timeElapsed += (decimal)Time.deltaTime;
        timeDisplay.text = "Time: " + Math.Round(timeElapsed, 1, MidpointRounding.ToEven);
    }
}
