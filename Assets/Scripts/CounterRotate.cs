using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterRotate : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(
            0.0f, 
            0.0f, 
            transform.rotation.eulerAngles.z + 
            transform.parent.rotation.eulerAngles.z * -1.0f
        );
    }
}
