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
            // todo
        }
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        handleCollision(other);
    }
    
    private void OnCollisionStay2D(Collision2D other) {
        handleCollision(other);
    }

    void handleCollision(Collision2D other) {
        if (LayerMask.LayerToName(other.gameObject.layer) == "guns") {
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
