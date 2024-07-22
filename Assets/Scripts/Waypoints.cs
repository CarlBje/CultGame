using UnityEngine;

public class WaypointGizmos : MonoBehaviour
{
    // The radius of the waypoint circles
    public float waypointRadius = 0.3f;

    // The color of the waypoint circles
    public Color waypointColor = Color.red;

    // The color of the lines connecting waypoints
    public Color lineColor = Color.green;

    // This method is called to draw gizmos that are also pickable and always drawn
    private void OnDrawGizmos()
    {
        // Save the current Gizmos color
        Color originalGizmoColor = Gizmos.color;

        // Get all child waypoints
        Transform[] waypoints = GetComponentsInChildren<Transform>();

        // Draw the waypoints
        Gizmos.color = waypointColor;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != transform) // Avoid drawing a gizmo for the parent object
            {
                Gizmos.DrawSphere(waypoints[i].position, waypointRadius);
            }
        }

        // Draw the lines between waypoints
        Gizmos.color = lineColor;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] != transform && waypoints[i + 1] != transform)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }

        // Restore the original Gizmos color
        Gizmos.color = originalGizmoColor;
    }
}
