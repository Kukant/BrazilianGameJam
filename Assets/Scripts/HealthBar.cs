using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private PlayerHealth ph;
    // Start is called before the first frame update
    void Start() {
        ph = GetComponentInParent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update() {
        var ls = transform.localScale;
        transform.localScale = new Vector3( ph.health/ph.initialHealth, ls.y, ls.z);
    }
}
