using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    public GameObject player;
    private Rigidbody playerRB;

    // Movement speed in units per second.
    public float speed = 1.0F;

    public float smoothTime = 0.3f;

    void Start()
    {
        playerRB = player.GetComponent<Rigidbody>();
    }

    // Move to the target end position.
    void LateUpdate()
    {
        Vector3 velocity = playerRB.velocity;
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, smoothTime);
    }
}
