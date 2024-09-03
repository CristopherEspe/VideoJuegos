using UnityEngine;
using System.Collections;

public class AutoMovement : MonoBehaviour
{
    [System.Serializable]
    public struct Waypoint
    {
        public Transform point;
        public float waitTime;
    }

    public Waypoint[] waypoints;
    public float speed = 2.0f;
    private int currentWaypointIndex = 0;
    private bool isWaiting = false;

    void Update()
    {
        if (!isWaiting && currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex].point;
            Vector3 direction = targetWaypoint.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                StartCoroutine(WaitAtWaypoint(waypoints[currentWaypointIndex].waitTime));
            }
        }
    }

    private IEnumerator WaitAtWaypoint(float waitTime)
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        currentWaypointIndex++;
        isWaiting = false;
    }
}
