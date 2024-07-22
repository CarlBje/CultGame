using UnityEngine;

public class WaypointGizmos : MonoBehaviour
{
    public float waypointRadius = 0.3f;

    public Color waypointColor = Color.red;

    public Color lineColor = Color.green;

    private void OnDrawGizmos()
    {
        Color originalGizmoColor = Gizmos.color;

        Transform[] waypoints = GetComponentsInChildren<Transform>();

        Gizmos.color = waypointColor;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != transform)
            {
                Gizmos.DrawSphere(waypoints[i].position, waypointRadius);
            }
        }

        Gizmos.color = lineColor;
        for (int i = 0; i < waypoints.Length - 1; i++)
        {
            if (waypoints[i] != transform && waypoints[i + 1] != transform)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }

        Gizmos.color = originalGizmoColor;
    }
}
