using UnityEngine;

public class LightPulse : MonoBehaviour
{
    public enum State
    {
        Increase,
        Decrease
    }

    [SerializeField]
    private float pulseSpeed = 10.0f;
    [SerializeField]
    private float lightRangeMax = 10.0f;
    [SerializeField]
    private float lightRangeMin = 2.0f;

    private State state = State.Increase;

    [SerializeField]
    private Light pointLight;

    private void Awake()
    {
        if (pointLight == null)
        {
            pointLight = GetComponent<Light>();
            Debug.Log("Warning: " + gameObject.name + "'s light was not assigned.");

            if (pointLight == null)
            {
                Debug.Log("Warning: " + gameObject.name + " could not find point light in child GameObjects.");
            }
        }

        pointLight.range = lightRangeMin;
    }

    private void OnDisable()
    {
        pointLight.range = lightRangeMin;
    }

    private void Update()
    {
        Pulse();
    }

    private void Pulse()
    {
        switch (state)
        {
            case State.Increase:
                {
                    pointLight.range += pulseSpeed * Time.deltaTime;

                    if (pointLight.range >= lightRangeMax)
                    {
                        state = State.Decrease;
                    }
                    break;
                }

            case State.Decrease:
                {
                    pointLight.range -= pulseSpeed * Time.deltaTime;

                    if (pointLight.range <= lightRangeMin)
                    {
                        state = State.Increase;
                    }
                    break;
                }
        }
    }
}
