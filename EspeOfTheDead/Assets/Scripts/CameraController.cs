using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    public Transform bulletSpawn;
    public float rotationSpeed;
    public float proportionEdgeSize;

    private void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        Vector3 mousePosition = Input.mousePosition;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float horizontalRotation = 0f;
        float verticalRotation = 0f;

        // Right and left edges
        if (mousePosition.x <= proportionEdgeSize)
        {
            horizontalRotation = -1f;
        }
        else if (mousePosition.x >= screenWidth - proportionEdgeSize)
        {
            horizontalRotation = 1f;
        }

        // Upper and lower edges
        if (mousePosition.y <= proportionEdgeSize)
        {
            verticalRotation = 1f;
        }
        else if (mousePosition.y >= screenHeight - proportionEdgeSize)
        {
            verticalRotation = -1f;
        }


        if (horizontalRotation != 0f)
        {
            transform.RotateAround(player.position, Vector3.up, horizontalRotation * rotationSpeed * Time.deltaTime);
            transform.RotateAround(bulletSpawn.position, Vector3.up, horizontalRotation * rotationSpeed * Time.deltaTime);
        }
        
        if (verticalRotation != 0f)
        {
            Vector3 right = transform.right;
            transform.RotateAround(player.position, right, verticalRotation * rotationSpeed * Time.deltaTime);
            transform.RotateAround(bulletSpawn.position, right, verticalRotation * rotationSpeed * Time.deltaTime);
        }
    }
}
