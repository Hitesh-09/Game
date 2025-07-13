using UnityEngine;

public class CarWaypoint : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public float reachThreshold = 1.5f;

    public Transform frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;

    private int currentWaypointIndex = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];
        Vector3 direction = (target.position - transform.position).normalized;

        // Move car using physics
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        // Face the direction
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, lookRotation, Time.fixedDeltaTime * 5f));
        }

        // Check distance to waypoint
        if (Vector3.Distance(transform.position, target.position) < reachThreshold)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                enabled = false; // stop at last waypoint
            }
        }

        RotateWheels();
        if (rb.linearVelocity.magnitude > 0.1f)
{
    // rotate wheels
}

    }

    void RotateWheels()
    {
        float rotationSpeed = speed * 360 * Time.deltaTime / (2 * Mathf.PI * 0.33f); // Approx wheel radius = 0.33m
        frontLeftWheel.Rotate(Vector3.right, rotationSpeed);
        frontRightWheel.Rotate(Vector3.right, rotationSpeed);
        rearLeftWheel.Rotate(Vector3.right, rotationSpeed);
        rearRightWheel.Rotate(Vector3.right, rotationSpeed);
    }
}
