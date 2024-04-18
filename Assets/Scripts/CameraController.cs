using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Reference to the player sphere GameObject
    public GameObject guide; // Reference to the satellite sphere GameObject

    private Rigidbody playerRB;

    private float maxDistance; // Maximum distance between camera and player sphere
    private float distance; // Distance between camera and player
    public float sensitivityX = 5f; // Mouse sensitivity for horizontal movement
    public float sensitivityY = 5f; // Mouse sensitivity for vertical movement
    public float minYAngle = -60f; // Minimum vertical angle for the camera
    public float maxYAngle = 60f; // Maximum vertical angle for the camera

    private float currentX = 0f; // Current horizontal rotation of the camera
    private float currentY = 0f; // Current vertical rotation of the camera

    private Vector3 guideOffset, targetGuidePosition;

    public float smoothTime = 0.3f;

    private void Start()
    {
        // Get calculations from scene
        maxDistance = Vector3.Distance(transform.position, player.transform.position);
        guideOffset = guide.transform.position - player.transform.position;

        playerRB = player.GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        // Get mouse input for camera rotation
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY -= Input.GetAxis("Mouse Y") * sensitivityY;
        
        // Clamp vertical rotation angle within limits
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);

        // Calculate camera rotation based on mouse input
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        // Check for clipping
        RaycastHit hit;
        /*if (Physics.Raycast(player.transform.position, transform.position, out hit, maxDistance))
            distance = hit.distance;
        else
            distance = maxDistance;*/
        distance = Physics.Raycast(guide.transform.position, transform.position, out hit, maxDistance, ~LayerMask.NameToLayer("UI")) ? hit.distance : maxDistance;

        // Set camera position relative to player sphere
        transform.position = player.transform.position - rotation * Vector3.forward * distance;

        // Calculate guide position relative to player sphere
        targetGuidePosition = player.transform.position + rotation * guideOffset;

        // Smoothly move the guide towards its target position
        Vector3 velocity = playerRB.velocity;
        guide.transform.position = Vector3.SmoothDamp(guide.transform.position, targetGuidePosition, ref velocity, smoothTime);

        // Look at the guide sphere to maintain right triangle relationship
        transform.LookAt(guide.transform);
    }

}
