using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform waypointParent;
    public float speed = 2.0f;
    public float reachThreshold = 0.1f; // Threshold to consider waypoint reached
    public float detectionRadius = 0.49f; // Radius to detect other gameobjects
    public float detectionAngle = 45.0f; // Angle within which to detect objects in front

    private Transform[] waypoints;
    private int currentWaypointIndex;
    private bool move = true;

    public Sprite[] sprites;

    void Awake()
    {
        //Pick a random sprite
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        //set sprite size to 0.25
        transform.localScale = new Vector3(0.25f, 0.25f, 1);
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            if (waypointParent != null)
            {
                InitializeWaypoints();
            }
            else
            {
                return;
            }
        }

        bool enemyNearby = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject == this.gameObject)
            {
                // Ignore self
                continue;
            }

            if ((collider.CompareTag("Monster") || collider.CompareTag("Enemy")) && IsObjectInFront(collider.transform))
            {
                enemyNearby = true;
                break;
            }
        }

        move = !enemyNearby;

        if (!move) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, step);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < reachThreshold)
        {
            if (currentWaypointIndex > 0)
            {
                currentWaypointIndex--;
            }
            else
            {
                move = false;
            }
        }
    }

    private void InitializeWaypoints()
    {
        if (waypointParent != null)
        {
            int childCount = waypointParent.childCount;
            waypoints = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                waypoints[i] = waypointParent.GetChild(i);
            }
            currentWaypointIndex = waypoints.Length - 1;
        }
    }

    private bool IsObjectInFront(Transform obj)
    {
        Vector2 directionToObj = obj.position - transform.position;
        Vector2 forwardDirection = waypoints[currentWaypointIndex].position - transform.position;

        float angle = Vector2.Angle(forwardDirection, directionToObj);

        return angle < detectionAngle / 2.0f && directionToObj.magnitude < detectionRadius;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (waypoints != null && waypoints.Length > 0)
        {
            Vector3 forwardDirection = waypoints[currentWaypointIndex].position - transform.position;

            Quaternion leftRayRotation = Quaternion.AngleAxis(-detectionAngle / 2, Vector3.forward);
            Quaternion rightRayRotation = Quaternion.AngleAxis(detectionAngle / 2, Vector3.forward);

            Vector3 leftRayDirection = leftRayRotation * forwardDirection.normalized * detectionRadius;
            Vector3 rightRayDirection = rightRayRotation * forwardDirection.normalized * detectionRadius;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + leftRayDirection);
            Gizmos.DrawLine(transform.position, transform.position + rightRayDirection);

            DrawSectorGizmo(forwardDirection, detectionAngle, detectionRadius);
        }
    }

    private void DrawSectorGizmo(Vector3 forwardDirection, float angle, float radius)
    {
        int segments = 20;
        float segmentAngle = angle / segments;

        Vector3 previousPoint = transform.position + Quaternion.AngleAxis(-angle / 2, Vector3.forward) * forwardDirection.normalized * radius;

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = -angle / 2 + segmentAngle * i;
            Vector3 currentPoint = transform.position + Quaternion.AngleAxis(currentAngle, Vector3.forward) * forwardDirection.normalized * radius;
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }

    public void SetWaypointParent(Transform parent)
    {
        waypointParent = parent;
        InitializeWaypoints();
    }
}
