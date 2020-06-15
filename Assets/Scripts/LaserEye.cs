using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LaserEye : MonoBehaviour
{
    public float Speed = 0.01f;
    public RuntimeAnimatorController NewController;
    private Animator animator;
    private bool fire, first = true;

    private Transform upTrans;

    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(0);
        StartCoroutine(WaitForAnim());
        upTrans = GameObject.Find("ultraplex")?.transform;
        if (!upTrans) {
            Destroy(gameObject);
            return;
        }

        movement = (upTrans.position - transform.position).normalized;

        float rot_z = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fire) {
            transform.position += movement * Speed;
        }
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(
            animator.GetCurrentAnimatorStateInfo(0).length
            +
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        fire = true;
        animator.runtimeAnimatorController = NewController;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
