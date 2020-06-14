using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float health = 100;
    public GameObject explosion;
    private GameController gc;

    private void Start() {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Die() {
        if (explosion != null) {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        
        Destroy(gameObject);
    }
    
    
    private void OnTriggerStay2D(Collider2D other) {
        handleCollision(other.gameObject);
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        handleCollision(other.gameObject);
    }

    void handleCollision(GameObject other) {
        if (LayerMask.LayerToName(other.layer) == "guns") {
            var pre = health;
            health -= other.gameObject.GetComponent<Damage>().damage;
            if (health < 0) {
                health = 0;
            }

            var damageDone = pre - health;
            gc.AddScore((int)damageDone);

            if (health <= 0) {
                Die();
            }
        }
    }
}
