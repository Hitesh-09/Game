using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SmartCarAI : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 5f;
    public Transform[] wheels;
    public float wheelRotationMultiplier = 360f;

    private List<Transform> path = new List<Transform>();
    private Transform targetWaypoint;
    private int currentIndex = 0;

    void Start()
    {
        // Step 1: Find all waypoints
        GameObject[] allWaypoints = GameObject.FindGameObjectsWithTag("Waypoint");

        // Step 2: Find closest waypoint to start
        Transform closest = FindClosestWaypoint(allWaypoints);
        
        // Step 3: Find a random destination waypoint (or set manually)
        Transform destination = allWaypoints[Random.Range(0, allWaypoints.Length)].transform;

        // Step 4: Generate path from closest to destination
        path = PathFinder.FindPath(closest, destination);

        if (path != null && path.Count > 0)
        {
            targetWaypoint = path[0];
            currentIndex = 0;
        }
    }

    void Update()
    {
        if (targetWaypoint == null) return;

        Vector3 dir = (targetWaypoint.position - transform.position).normalized;
        float step = speed * Time.deltaTime;

        // Move forward
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        // Rotate smoothly
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Rotate wheels visually
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right, speed * wheelRotationMultiplier * Time.deltaTime);
        }

        // Go to next waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 1f)
        {
            currentIndex++;
            if (currentIndex < path.Count)
                targetWaypoint = path[currentIndex];
            else
                targetWaypoint = null; // Arrived
        }
    }

    Transform FindClosestWaypoint(GameObject[] waypoints)
    {
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject wp in waypoints)
        {
            float dist = Vector3.Distance(transform.position, wp.transform.position);
            if (dist < minDist)
            {
                closest = wp.transform;
                minDist = dist;
            }
        }
        return closest;
    }
}
