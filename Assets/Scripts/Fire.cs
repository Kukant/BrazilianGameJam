using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed = (2 * Mathf.PI) / 5; // 5-seconds to complete the circle;
    public float Radius = 3.5f;

    private float angle = 0;

    void Start()
    {
        transform.GetComponent<Animator>().enabled = false;  
    }
    // Update is called once per frame
    void Update()
    {
        var parent = transform.parent.GetComponent<Enemy1>();
        if (!parent.inside)
        {
            transform.localPosition = CircleOffset(Time.deltaTime);
        }
    }

    private Vector2 CircleOffset(float delta)
    {
        angle += speed * delta;
        return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
