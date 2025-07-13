using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
    public static List<Transform> FindPath(Transform start, Transform end)
    {
        Waypoint startWP = start.GetComponent<Waypoint>();
        Waypoint endWP = end.GetComponent<Waypoint>();

        if (startWP == null || endWP == null)
        {
            Debug.LogError("Start or End does not have a Waypoint script.");
            return null;
        }

        Queue<Waypoint> queue = new Queue<Waypoint>();
        Dictionary<Waypoint, Waypoint> cameFrom = new Dictionary<Waypoint, Waypoint>();
        HashSet<Waypoint> visited = new HashSet<Waypoint>();

        queue.Enqueue(startWP);
        visited.Add(startWP);

        while (queue.Count > 0)
        {
            Waypoint current = queue.Dequeue();

            if (current == endWP)
            {
                // Backtrack the path
                List<Transform> path = new List<Transform>();
                Waypoint step = endWP;

                while (step != null)
                {
                    path.Insert(0, step.transform);
                    cameFrom.TryGetValue(step, out step);
                }

                return path;
            }

            foreach (Waypoint neighbor in current.neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    cameFrom[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        Debug.LogWarning("No path found.");
        return null;
    }
}
