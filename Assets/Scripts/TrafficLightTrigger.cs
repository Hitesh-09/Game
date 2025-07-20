using UnityEngine;

public class TrafficLightTrigger : MonoBehaviour
{
    public SimpleTrafficLightController trafficLight; // Reference to the controller

    void OnTriggerStay(Collider other)
    {
        CarPathFollower car = other.GetComponent<CarPathFollower>();
        if (car != null)
        {
            Debug.Log("Car Detected in Trigger Zone");

            if (trafficLight.IsGreen)
            {
                Debug.Log("Light is GREEN — Car Moves");
                car.SetCanMove(true); // Allow movement
            }
            else
            {
                Debug.Log("Light is RED — Car Stops");
                car.SetCanMove(false); // Stop at red
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        CarPathFollower car = other.GetComponent<CarPathFollower>();
        if (car != null)
        {
            car.SetCanMove(true); // Resume after exiting
        }
    }
}
