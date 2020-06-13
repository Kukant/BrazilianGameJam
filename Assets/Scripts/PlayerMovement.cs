using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 force = new Vector3(100, 0, 0);
    public float minSpeed;
    public float forceMultiplier;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // only the x direction is interesting
        var speed = rb.velocity.x;      //to get a Vector3 representation of the velocity


        if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector3.left * forceMultiplier);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector3.right * forceMultiplier);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector3.up * forceMultiplier);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector3.down * forceMultiplier);
    }
}
