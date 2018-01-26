using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    private decimal timeElapsed = 0;

    [SerializeField]
    private Text timeDisplay;

    private void Update()
    {
        if (GameManager.instance.GameIsRunning)
        {
            timeElapsed += (decimal)Time.deltaTime;
            timeDisplay.text = "Time: " + Math.Round(timeElapsed, 1, MidpointRounding.ToEven);
        }
    }
}
