using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public static Transform[] sharedWaypoints;

    void Awake()
    {
        // Get all child waypoints in order
        int count = transform.childCount;
        sharedWaypoints = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            sharedWaypoints[i] = transform.GetChild(i);
        }
    }
}
