using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        var bla = other.GetComponent<Enemy1>();
        if (bla != null) {
            bla.IsActive = true;
        }
    }
}
