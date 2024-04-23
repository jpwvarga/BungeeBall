using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Info")]
    private Transform mainCam;
    private Rigidbody rb;
    [SerializeField] GameController gc;

    [Header("Movement")]
    public float speed = 0;
    private float sideness, forwardness;
    [SerializeField] float jumpStrength = 200;
    private bool isGrounded = true;
    [SerializeField] float groundTolerance = 0.2f;
    private RaycastHit groundHit;
    private bool isBraking = false;
    [SerializeField] float brakeAngularDrag = 2f;
    private float originalAngularDrag;

    [Header("Grappling")]
    public GrapplingHook hook;

    
    void Start()
    {
        mainCam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (gc.IsOver()) return;

        sideness = Input.GetAxis("Horizontal");
        forwardness = Input.GetAxis("Vertical");

        if (isGrounded && Input.GetButtonUp("Jump"))
        {
           //Debug.Log("Boing");
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Z))
        {
            Brake();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.Z))
        {
            Unbrake();
        }
    }

    void FixedUpdate()
    {
        if (gc.IsOver())
        {
            Brake();
            return;
        }

        isGrounded = Physics.Raycast(transform.position, Vector3.down, out groundHit, transform.localScale.y / 2 + groundTolerance, ~LayerMask.NameToLayer("Ignore Raycast"));

        if (!isBraking)
            rb.AddForce((mainCam.forward * forwardness + mainCam.right * sideness) * speed);
        else
            Brake();
    }

    private void Jump()
    {
        rb.AddForce(jumpStrength * rb.mass * Vector3.up);
    }

    private void Brake()
    {
        if (isGrounded)
        {
            if (!isBraking)
            {
                isBraking = true;
                originalAngularDrag = rb.angularDrag;
            }

            rb.angularDrag = brakeAngularDrag;
        }
        else if (isBraking)
        {
            Unbrake();
        }
    }

    private void Unbrake()
    {
        rb.angularDrag = originalAngularDrag;
        isBraking = false;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
