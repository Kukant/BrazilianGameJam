using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading;
using Random = System.Random;

public class Enemy1 : MonoBehaviour
{
    public bool IsActive;
    public string Hero = "Hero";
    public float speed = (2 * Mathf.PI) / 5; // 5-seconds to complete the circle;
    public float Radius = 2.5f;

    private Vector2 centre;
    private float angle = 0;

    public GameObject Spawnee;
    public int MaxNumberOfSpawnees = 6;
    public int CurrentNumberOfSpawnees = 0;
    public int Delay = 15;
    private int delay = 0;
    public bool inside, right;
    private int iter = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        float rand = UnityEngine.Random.value;
        Debug.Log(rand);
        if (rand > 0.5)
        {
            right = true;
        }
        centre = transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //when triggered
        if (IsActive)
        {
            // circle motion
            if (right)
            {
                transform.position = centre + CircleOffset(Time.deltaTime, Radius);
            }
            else
            {
                transform.position =  centre + CircleOffset2(Time.deltaTime, Radius);
            }

            // fire objects
            if (CurrentNumberOfSpawnees <= MaxNumberOfSpawnees)
            {
                iter = 0; 
                if (delay == Delay)
                {
                    GameObject fire = Instantiate(Spawnee);
                    fire.name = string.Format("fire {0}", CurrentNumberOfSpawnees + 1);
                    fire.transform.SetParent(transform);
                    
                    Vector2 positionOffset = CircleOffset(Time.deltaTime, Radius);
                    angle -= speed * Time.deltaTime;
                    fire.transform.localPosition = new Vector3(positionOffset.x, positionOffset.y, 0);
                    CurrentNumberOfSpawnees++;
                    delay = 0;
                }
            }
            else
            {
                inside = true;
                foreach (Transform child in transform)
                {
                    child.position = Vector2.MoveTowards(child.position, transform.position, -1 * 15 * Time.deltaTime);
                    if (transform.childCount > iter)
                    {
                        float angle = Vector3.Angle(Vector3.right, child.position - transform.position);
                        if (transform.position.y - child.position.y > 0)
                        {
                            child.rotation = Quaternion.Euler(0, 0, 360 - angle);
                        }
                        else
                        {
                            child.rotation = Quaternion.Euler(0, 0, angle);
                        }
                        iter++;
                    }
                    child.GetComponent<Animator>().enabled = true;
                }
                if (transform.childCount == 0)
                {
                    inside = false;
                    CurrentNumberOfSpawnees = 0;
                    delay = 0;
                }
            }
            delay++;
        }
    }

    private Vector2 CircleOffset( float delta, float radius)
    {
        angle += speed * delta;
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
    }
    private Vector2 CircleOffset2(float delta, float radius)
    {
        angle += speed * delta;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle) ) * radius;
    }


    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        // Debug-draw all contact points and normals
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

}


