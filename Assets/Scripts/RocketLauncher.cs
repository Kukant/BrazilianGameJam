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

    private Quaternion currentRotation;
    private bool shooting = false;
    
    public void Start() {
        lastLaunch = DateTime.Now;
        playerRB = GameObject.Find("Player(Clone)").GetComponent<Rigidbody2D>();
    }

    public void Activate(bool on) {
        GetComponent<SpriteRenderer>().enabled = on;
        transform.localRotation = Quaternion.Euler(0, 0, -90);
    }

    private void LateUpdate() {
        if (shooting) {
            transform.rotation = currentRotation;
        }
    }


    public void Launch() {
        TimeSpan fromLastLaunch = DateTime.Now - lastLaunch;
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
        StartCoroutine(_launchProcedure(angle));
    }
    
    IEnumerator _launchProcedure(int angle) {
        shooting = true;
        
        if (angle == 90) {
            currentRotation = Quaternion.Euler(0, 180, 270);
        } else {
            currentRotation = Quaternion.Euler(0, 0, angle);
        }
        
        yield return new WaitForSeconds(0.2f);
        
        
        var newpos = transform.position;
        var diff = 2f;
        if (angle == 0) {
            newpos.y += diff;
        } else if (angle == 180) {
            newpos.y -= diff;
        } else if (angle == 90) {
            newpos.x -= diff;
        } else if (angle == 270) {
            newpos.x += diff;
        }
        
        

        var r = Instantiate(rocketPrefab, newpos, Quaternion.Euler(0, 0, angle));
        MusicController.SoundController(MusicController.SOUNDS.ROCKET_LAUNCH, true);
        r.GetComponent<Rigidbody2D>().velocity = playerRB.velocity;
        
        
        yield return new WaitForSeconds(0.4f);
        
        
        shooting = false;
        transform.localRotation = Quaternion.Euler(0, 0, -90);
    }
}
