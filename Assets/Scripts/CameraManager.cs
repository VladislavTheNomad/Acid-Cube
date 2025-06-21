using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float smoothSpeed = 5f; // speed of movement to the player's position
    private Vector3 standardCameraPosition = new Vector3 (0, 2f, 10f); // Initializated position, DONT CHANGE
    private Vector3 destinationPoint; // the camera point is compared to the player's position EACH Update
    private Vector3 currentPosition; // the position of the camera as it moves between the player's position and the starting position

    void LateUpdate()
    {
        //transform.position = player.transform.position + new Vector3(0, 2f, 10f);

        destinationPoint = player.transform.position + standardCameraPosition;
        currentPosition = Vector3.Lerp(transform.position, destinationPoint, smoothSpeed * Time.deltaTime);
        transform.position = currentPosition;
    }
}
