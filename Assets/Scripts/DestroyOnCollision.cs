using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

    public GameObject explosion;
    
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other) {
        if (!other.collider.isTrigger) {
            if (explosion) {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
