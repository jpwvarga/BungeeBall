using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Reference to the player sphere GameObject

    public float cameraSpeed = 120f;
    
    public float sensitivityX = 300f; // Mouse sensitivity for horizontal movement
    public float sensitivityY = 300f; // Mouse sensitivity for vertical movement
    public float minYAngle = -60f; // Minimum vertical angle for the camera
    public float maxYAngle = 60f; // Maximum vertical angle for the camera

    private float mouseX, mouseY;
    private float finalInputX, finalInputZ;
    private float rotX = 0f; // Current horizontal rotation of the camera
    private float rotY = 0f; // Current vertical rotation of the camera

    private Vector3 targetGuidePosition;
    public float guideOffset;

    public float smoothTime = 0.5f;

    void Awake()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }

    void Update()
    {
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotY += finalInputX * sensitivityX * Time.deltaTime;
        rotX += finalInputZ * sensitivityY * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, minYAngle, maxYAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        // Calculate guide position relative to player sphere
        targetGuidePosition = player.transform.position + Vector3.Cross(transform.forward, transform.right).normalized * guideOffset;

        // Move base towards guide
        transform.position = Vector3.MoveTowards(transform.position, targetGuidePosition, smoothTime);
    }

}
