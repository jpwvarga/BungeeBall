using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Info")]
    private Transform mainCam;
    private Rigidbody rb;

    [Header("Movement")]
    public float speed = 0;
    private float sideness, forwardness;
    public float jumpStrength = 200;
    private bool isGrounded = true;
    public float groundTolerance = 0.2f;
    private RaycastHit groundHit;

    [Header("Grappling")]
    public GrapplingHook hook;

    
    void Start()
    {
        mainCam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        sideness = Input.GetAxis("Horizontal");
        forwardness = Input.GetAxis("Vertical");

        if (isGrounded && Input.GetButtonUp("Jump"))
        {
            //Debug.Log("Boing");
            Jump();
        }
    }

    void FixedUpdate()
    {
        rb.AddForce((mainCam.forward * forwardness + mainCam.right * sideness) * speed);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, out groundHit, transform.localScale.y / 2 + groundTolerance, ~LayerMask.NameToLayer("Ignore Raycast"));
    }

    private void Jump()
    {
        rb.AddForce(jumpStrength * rb.mass * Vector3.up);
    }
}
