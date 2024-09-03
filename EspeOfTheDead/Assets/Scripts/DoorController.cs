using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform[] waypoints; // Waypoints for player path
    public GameObject[] doorsToOpen; // Doors to be rotated
    public int[] doorWaypointsIndices; // Waypoints that trigger door opening
    public float doorOpenAngle = 90f; // Angle to open the door
    public float doorOpenSpeed = 2f; // Speed of door opening

    private bool[] doorsOpened;

    void Start()
    {
        doorsOpened = new bool[doorsToOpen.Length];
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
        for (int i = 0; i < doorsToOpen.Length; i++)
        {
            if (System.Array.Exists(doorWaypointsIndices, index => index == waypointIndex) && !doorsOpened[i])
            {
                StartCoroutine(RotateDoor(doorsToOpen[i], doorOpenAngle));
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
