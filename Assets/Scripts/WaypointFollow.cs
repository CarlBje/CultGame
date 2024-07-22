using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    public Transform waypointParent; // Parent object containing waypoints
    public float speed = 2.0f; // Movement speed
    public float reachThreshold = 0.1f; // Threshold to consider waypoint reached

    private Transform[] waypoints; // Array of waypoints
    private int currentWaypointIndex = 0; // Index of the current waypoint
    private bool isMoving = true; // Flag to check if the object should continue moving

    void Start()
    {
        // Get all child waypoints from the waypoint parent
        if (waypointParent != null)
        {
            int childCount = waypointParent.childCount;
            waypoints = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                waypoints[i] = waypointParent.GetChild(i);
            }
        }
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0 || !isMoving) return; // If no waypoints or should not move, do nothing

        // Get the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Move towards the current waypoint
        float step = speed * Time.deltaTime; // Calculate distance to move
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, step);

        // Check if the waypoint is reached
        if (Vector2.Distance(transform.position, targetWaypoint.position) < reachThreshold)
        {
            // Move to the next waypoint if it exists
            if (currentWaypointIndex < waypoints.Length - 1)
            {
                currentWaypointIndex++;
            }
            else
            {
                // Stop moving if the last waypoint is reached
                isMoving = false;
            }
        }
    }

}
