using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RocketLauncher : MonoBehaviour {
    private DateTime lastLaunch;
    public GameObject rocketPrefab;
    public int cooldownMills = 1000;
    private Rigidbody2D playerRB;
    public void Start() {
        lastLaunch = DateTime.Now;
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    public void Launch() {
        TimeSpan fromLastLaunch = DateTime.Now - lastLaunch;
        print(fromLastLaunch.Milliseconds);
        if (fromLastLaunch.TotalMilliseconds < cooldownMills) {
            return;
        }
        lastLaunch = DateTime.Now;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Keypad8)) {
            _launch(0);
        } else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Keypad5)) {
            _launch(180);
        } else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Keypad4)) {
            _launch(90);
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Keypad6)) {
            _launch(270);
        }
    }

    private void _launch(int angle) {
        var r = Instantiate(rocketPrefab, transform.position, Quaternion.Euler(0, 0, angle));
        r.GetComponent<Rigidbody2D>().velocity = playerRB.velocity;
    }
}
