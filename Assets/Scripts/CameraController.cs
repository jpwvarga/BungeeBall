using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player sphere GameObject
    public Transform guide; // Reference to the satellite sphere GameObject

    private float distance; // Distance between camera and player sphere
    public float sensitivityX = 5f; // Mouse sensitivity for horizontal movement
    public float sensitivityY = 5f; // Mouse sensitivity for vertical movement
    public float minYAngle = -60f; // Minimum vertical angle for the camera
    public float maxYAngle = 60f; // Maximum vertical angle for the camera

    private float currentX = 0f; // Current horizontal rotation of the camera
    private float currentY = 0f; // Current vertical rotation of the camera

    private Vector3 guideOffset, targetGuidePosition;

    public float guideLerpSpeed = 15;

    private void Start()
    {
        // Get calculations from scene
        distance = Vector3.Distance(transform.position, player.position);
        guideOffset = guide.position - player.position;
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
        
        // Set camera position relative to player sphere
        transform.position = player.position - rotation * Vector3.forward * distance;

        // Calculate guide position relative to player sphere
        targetGuidePosition = player.position + rotation * guideOffset;

        // Smoothly move the guide towards its target position
        guide.position = Vector3.Lerp(guide.position, targetGuidePosition, Time.deltaTime * guideLerpSpeed);

        // Look at the guide sphere to maintain right triangle relationship
        transform.LookAt(guide);
    }

}
