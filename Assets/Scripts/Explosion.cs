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
        MusicController.SoundController(MusicController.SOUNDS.EXPLOSION, true);
        StartCoroutine(WaitForAnim());
        StartCoroutine(TurnOffCollider());
    }
    
    
    IEnumerator TurnOffCollider()
    {
        yield return new WaitForSeconds(0.5f);
        var col = transform.GetComponent<CircleCollider2D>();
        if (col)
            col.enabled = false;
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
