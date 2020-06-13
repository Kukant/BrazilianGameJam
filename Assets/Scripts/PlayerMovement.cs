using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public float forceMultiplier;

    private Rigidbody2D rb;
    private CameraMovement cm;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cm = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // only the x direction is interesting
        var speed = rb.velocity.x;      //to get a Vector3 representation of the velocity

        var moving = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) ||
                     Input.GetKey(KeyCode.S);

        if (!moving && speed < 2) {
            transform.position = transform.position + new Vector3(cm.speed, 0, 0);
        }

        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(Vector3.left * forceMultiplier);
        }


        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(2f * Vector3.right * forceMultiplier);
        }


        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(Vector3.up * forceMultiplier);
        }


        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(Vector3.down * forceMultiplier);
        }

        
    }
}
