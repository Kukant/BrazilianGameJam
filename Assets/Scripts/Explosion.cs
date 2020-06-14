using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;
    void Start() {
        animator = GetComponent<Animator>();
        animator.Play(0);
        StartCoroutine(WaitForAnim());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // todo remove some health
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(
            animator.GetCurrentAnimatorStateInfo(0).length
            +
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        Destroy(gameObject);
    }

}
