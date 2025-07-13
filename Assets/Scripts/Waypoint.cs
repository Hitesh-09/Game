using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> neighbors = new List<Waypoint>();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (Waypoint neighbor in neighbors)
        {
            if (neighbor != null)
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }
}
