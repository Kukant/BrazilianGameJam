using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {
    public float force = 10f;
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public GameObject explosionPrefab;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        var normalized = transform.up.normalized;
        rb.AddForce(new Vector2(normalized.x, normalized.y) * force);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.isTrigger) {
            return;
        }

        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
