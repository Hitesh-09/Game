using UnityEngine;

public class CarPathFollower : MonoBehaviour
{
    public float speed = 5f;
    private int currentWaypointIndex = 0;

    private Transform[] waypoints;
    private bool canMove = true; // Used to control stopping for red light

    // Wheels
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    public float wheelRotationSpeed = 300f;

    void Start()
    {
        waypoints = RouteManager.sharedWaypoints; // Get shared route
    }

    void Update()
    {
        if (!canMove || waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypointIndex];

        // Move toward waypoint
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Face toward waypoint
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
            transform.forward = Vector3.Lerp(transform.forward, direction, 5f * Time.deltaTime);

        // Rotate wheels
        RotateWheels();

        // Switch to next waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
                currentWaypointIndex = 0; // Loop path
        }
    }

    void RotateWheels()
    {
        float rotationAmount = wheelRotationSpeed * Time.deltaTime;
        if (frontLeftWheel != null) frontLeftWheel.Rotate(Vector3.right, rotationAmount);
        if (frontRightWheel != null) frontRightWheel.Rotate(Vector3.right, rotationAmount);
        if (rearLeftWheel != null) rearLeftWheel.Rotate(Vector3.right, rotationAmount);
        if (rearRightWheel != null) rearRightWheel.Rotate(Vector3.right, rotationAmount);
    }

    // 🔴 Called by TrafficLightTrigger to control movement
    public void SetCanMove(bool value)
    {
        Debug.Log("SetCanMove called with: " + value);
        canMove = value;
    }
}

