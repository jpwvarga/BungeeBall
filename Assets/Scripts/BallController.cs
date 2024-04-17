using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Basic Info")]
    private Transform mainCam;
    private Rigidbody rb;

    [Header("Movement")]
    public float speed = 0;
    private float sideness, forwardness;
    public float jumpStrength = 200;
    private bool isGrounded;

    [Header("Grappling")]
    private bool isGrappling;

    
    void Start()
    {
        mainCam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        isGrappling = false;
    }

    void Update()
    {
        sideness = Input.GetAxis("Horizontal");
        forwardness = Input.GetAxis("Vertical");

        isGrounded = rb.velocity.y == 0;

        if(Input.GetButtonUp("Jump"))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        rb.AddForce((mainCam.forward * forwardness + mainCam.right * sideness) * speed);
    }

    // Either Jumps or Extends/Retracts grappling hook
    private void Jump()
    {
        if(isGrappling)
        {
            // Extend or Retract Grappling Hook
        }
        else if(isGrounded)
        {
            isGrounded = false;
            rb.AddForce(force: jumpStrength * rb.mass * Vector3.up);
            Debug.Log("Boing");
        }
    }
}
