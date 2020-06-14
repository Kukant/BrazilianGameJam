using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public bool isHurt;
    
    public float health = 100;
    public float initialHealth = 100;

    private void OnCollisionEnter2D(Collision2D other) {
        var damage = other.transform.gameObject.GetComponent<Damage>();
        isHurt = damage != null;
        if (damage != null) {
            health -= damage.damage;
        }
    }
}
