using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEye : MonoBehaviour
{
    public int Speed = 10;
    public RuntimeAnimatorController NewController;
    private Animator animator;
    private bool fire, first = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(0);
        StartCoroutine(WaitForAnim());
    }

    // Update is called once per frame
    void Update()
    {
        if (fire)
        {
            GameObject ultraplex = GameObject.Find("ultraplex");
            if (ultraplex != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.parent.position, -1 * Speed * Time.deltaTime);
                if (first)
                {
                    transform.position = Vector3.MoveTowards(transform.position, ultraplex.transform.position, Speed * Time.deltaTime);
                    float angle = Vector3.Angle(Vector3.up, ultraplex.transform.position - transform.position);
                    if (transform.parent.position.x < ultraplex.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 360 - angle);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, angle);
                    }
                    first = false;
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.parent.position, -1 * Speed * Time.deltaTime);
                if (first)
                {
                    float angle = Vector3.Angle(Vector3.up, transform.position - transform.parent.position);
                    Debug.Log(angle);
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                    first = false;
                }
            }
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
        transform.parent.GetComponent<Enemy2>().NextFire = true;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
