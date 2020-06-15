using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float Speed = 2.5f;
    public GameObject Spawnee;
    public bool NextFire = true;
    private bool active;
    private Rigidbody2D rgb;

    void Start()
    {
        rgb = transform.GetComponent<Rigidbody2D>();
        rgb.isKinematic = true;
        StartCoroutine(Shooting());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            if ((transform.rotation.eulerAngles.y >= 170 && transform.rotation.eulerAngles.z == 0) ||
                (transform.rotation.eulerAngles.z >= 170 && transform.rotation.eulerAngles.y == 0))
            {
                transform.position = transform.position + new Vector3(0.005f * Speed, 0, 0);
            }
            else
            {
                transform.position = transform.position - new Vector3(0.005f * Speed, 0, 0);
            }
        }
    }

    public void Activate()
    {
        active = true;
        rgb.isKinematic = false;
    }

    private void FireLaser()
    {
        GameObject laser;
        MusicController.SoundController(MusicController.SOUNDS.SNAKE_LASER, true);
        if (transform.rotation.eulerAngles.z >= 170)
        {
            laser = Instantiate(Spawnee, transform.position, Quaternion.Euler(0, 0, 180));
        }
        else
        {
            laser = Instantiate(Spawnee, transform.position, new Quaternion(0, 0, 0, 0));
        }
        laser.name = "laser";
        laser.transform.SetParent(transform);
    }
    
    IEnumerator Shooting()
    {
        yield return new WaitForSeconds(0.5f);
        if (active) {
            FireLaser();
        }
        StartCoroutine(Shooting());
    }
}
