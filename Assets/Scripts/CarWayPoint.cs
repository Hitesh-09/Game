using System.Collections.Generic;
using UnityEngine;

public class CarWaypoint : MonoBehaviour
{
    [Header("Waypoints the car will follow (assign manually in order)")]
    public List<Transform> waypoints = new List<Transform>();

    [Header("Car Movement Settings")]
    public float speed = 5f;
    public float turnSpeed = 5f;
    public float reachDistance = 1f;
    public bool loop = true;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Count == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - transform.position;

        // Move forward
        transform.position += direction.normalized * speed * Time.deltaTime;

        // Rotate toward the waypoint
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // If reached the current waypoint, go to the next
        if (direction.magnitude < reachDistance)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Count)
            {
                if (loop)
                    currentWaypointIndex = 0;
                else
                    enabled = false; // stop moving
            }
        }
    }
}
