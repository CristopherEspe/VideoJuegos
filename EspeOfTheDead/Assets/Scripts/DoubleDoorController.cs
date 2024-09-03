using UnityEngine;

public class DoubleDoorController : MonoBehaviour
{
    public Transform[] waypoints; // Waypoints for player path
    public GameObject[] leftDoors; // Left doors of double doors
    public GameObject[] rightDoors; // Right doors of double doors
    public int[] doorWaypointsIndices; // Waypoints that trigger door opening
    public float doorOpenAngle = 90f; // Angle to open the door
    public float doorOpenSpeed = 2f; // Speed of door opening

    private bool[] doorsOpened;

    void Start()
    {
        // Initialize doorsOpened array to keep track of opened doors
        doorsOpened = new bool[leftDoors.Length];
    }

    void Update()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, waypoints[i].position) < 0.1f)
            {
                OpenDoors(i);
            }
        }
    }

    void OpenDoors(int waypointIndex)
    {
        for (int i = 0; i < leftDoors.Length; i++)
        {
            // Check if the current waypoint index is in the array of indices for door openings
            if (System.Array.Exists(doorWaypointsIndices, index => index == waypointIndex) && !doorsOpened[i])
            {
                // Start coroutine to open both left and right doors
                StartCoroutine(RotateDoor(leftDoors[i], doorOpenAngle));
                StartCoroutine(RotateDoor(rightDoors[i], doorOpenAngle));
                doorsOpened[i] = true;
            }
        }
    }

    System.Collections.IEnumerator RotateDoor(GameObject door, float angle)
    {
        Quaternion startRotation = door.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(door.transform.eulerAngles + new Vector3(0, angle, 0));

        float elapsedTime = 0f;

        while (elapsedTime < doorOpenSpeed)
        {
            door.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / doorOpenSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.rotation = endRotation;
    }
}
