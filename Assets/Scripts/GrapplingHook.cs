using UnityEngine;
using TMPro;

public class GrapplingHook : MonoBehaviour
{
    public Transform grappleOrigin, player;
    private LineRenderer lr;
    private Vector3 grapplePoint;

    [Header("Grapple Settings")]
    public LayerMask layerMaskGrapplable;
    
    public float maxGrappleTime = 5f;
    private float currentGrappleTime = 0;
    public TMP_Text txtGrappleTimer;

    private float maxDistance = 30f;
    private SpringJoint joint;

    public float maxDist = 0.8f;
    public float minDist = 0.25f;
    public float spring = 4.5f;
    public float damper = 7f;
    public float massScale = 4.5f;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        txtGrappleTimer.text = "";
        currentGrappleTime = 0;
    }

    void Update()
    {
        if (joint)
        {
            currentGrappleTime += Time.deltaTime;
            txtGrappleTimer.text = (maxGrappleTime - currentGrappleTime).ToString("#0.00");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            StartGrapple();
        }
        else if (Input.GetButtonUp("Fire1") || currentGrappleTime > maxGrappleTime)
        {
            StopGrapple();
        }
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, layerMaskGrapplable))
        {
            // Create grapple rope
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint); 
            joint.maxDistance = distanceFromPoint * maxDist;
            joint.minDistance = distanceFromPoint * minDist;

            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            lr.positionCount = 2;
            currentGrapplePosition = grappleOrigin.position;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        currentGrappleTime = 0;
        txtGrappleTimer.text = "";
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, grappleOrigin.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}