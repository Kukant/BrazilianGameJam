using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public bool isHurt;
    
    public float health = 100;
    public float initialHealth = 100;
    public GameObject explosion;
    private void OnCollisionEnter2D(Collision2D other) {
        var damage = other.gameObject.GetComponent<Damage>();
        isHurt = damage != null;
        if (damage != null) {
            health -= damage.damage;

            if (health < 0) {
                Die();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        var damage = other.gameObject.GetComponent<Damage>();
        isHurt = damage != null;
        if (damage != null) {
            health -= damage.damage;

            if (health < 0) {
                Die();
            }
        }
    }

    void Die() {
        MusicController.SoundController(MusicController.SOUNDS.ULTRAPLEX_DEATH, true);
        var gc = GameObject.Find("GameController").GetComponent<GameController>();
        gc.RestartLevel1();
        if (explosion)
            Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
