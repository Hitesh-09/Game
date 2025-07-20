using UnityEngine;

public class SimpleTrafficLightController : MonoBehaviour
{
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;

    public float greenDuration = 5f;
    public float yellowDuration = 2f;
    public float redDuration = 5f;

    private enum LightState { Red, Green, Yellow }
    private LightState currentState;
    private float timer;

    public bool IsGreen => currentState == LightState.Green;

    void Start()
    {
        currentState = LightState.Red;
        timer = redDuration;
        UpdateLights();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            switch (currentState)
            {
                case LightState.Red:
                    currentState = LightState.Green;
                    timer = greenDuration;
                    break;
                case LightState.Green:
                    currentState = LightState.Yellow;
                    timer = yellowDuration;
                    break;
                case LightState.Yellow:
                    currentState = LightState.Red;
                    timer = redDuration;
                    break;
            }

            Debug.Log("Light changed to: " + currentState);
            UpdateLights();
        }
    }

    void UpdateLights()
    {
        redLight.SetActive(currentState == LightState.Red);
        yellowLight.SetActive(currentState == LightState.Yellow);
        greenLight.SetActive(currentState == LightState.Green);
    }
}
