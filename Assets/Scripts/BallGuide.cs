using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGuide : MonoBehaviour
{
    [SerializeField] Transform player; // Reference to the player's Transform
    [SerializeField] Transform cameraTransform; // Reference to the main camera's Transform
    private float distanceFromSurface; // Desired distance from the sphere's surface

    private void Start()
    {
        distanceFromSurface = Vector3.Distance(transform.position, player.position); 
    }

    void LateUpdate()
    {
        // Calculate the direction vector from the player to the camera
        Vector3 directionToCamera = cameraTransform.position - player.position;

        // Calculate the target position of the floating object
        Vector3 targetPosition = player.position + directionToCamera.normalized * distanceFromSurface;

        // Set the position of the floating object
        transform.position = targetPosition;

        // Calculate the rotation to look at the camera
        Quaternion targetRotation = Quaternion.LookRotation(transform.position - cameraTransform.position, Vector3.up);

        // Apply the rotation to the floating object
        transform.rotation = targetRotation;
    }
}
