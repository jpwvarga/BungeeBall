using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDistance = 1f; // Minimum distance between camera and guide
    public float maxDistance = 4f; // Maximum distance between camera and guide
    private float distance; // Distance between camera and player
    private Vector3 dollyDir;
    public float smoothTime = 0.5f;
    public GameObject cameraGuide;

    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = Vector3.Distance(transform.position, cameraGuide.transform.position);
    }

    void FixedUpdate()
    {
        // Try to move camera
        Vector3 targetCamPos = transform.parent.TransformPoint(dollyDir * maxDistance);

        // Check for clipping
        RaycastHit hit;
        if (Physics.Linecast(cameraGuide.transform.position, targetCamPos, out hit))
        {
            Debug.DrawLine(cameraGuide.transform.position, hit.point, Color.red);
            distance = Mathf.Clamp(hit.distance * 0.75f, minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smoothTime);
    }
}