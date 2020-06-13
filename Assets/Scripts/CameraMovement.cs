using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    
    private Transform playerTransform;        //Public variable to store a reference to the player game object
    private float maxRadius = 5f;
    public float goldSpeed = 0.1f;
    private float speed = 0.1f;
    private Camera thisCamera;
    private float cameraWidth;

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.Find("Player").transform;
        thisCamera = GetComponent<Camera>();
        cameraWidth = thisCamera.aspect * thisCamera.orthographicSize * 2;
    }
    
    // LateUpdate is called after Update each frame
    void FixedUpdate () {
        // right quarter
        if (playerTransform.position.x > transform.position.x + cameraWidth/3
            && speed < 1.7f * goldSpeed) {
            speed += 0.01f; 
        // left quarter
        } else if (playerTransform.position.x < transform.position.x - cameraWidth/4 &&
            speed > 0.5 * goldSpeed) {
            speed -= 0.01f;
        }
        else if (Math.Abs(speed - goldSpeed) > 0.01f) {
            speed += 0.005f * (speed < goldSpeed ? 1 : -1);
        }

        transform.position = transform.position + new Vector3(speed, 0, 0);
    }
}
