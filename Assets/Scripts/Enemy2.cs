using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float Speed = 2.5f;
    public GameObject Spawnee;
    public bool NextFire = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!NextFire)
        {
            transform.GetChild(transform.childCount - 1).position = transform.position;
        }
        if ((transform.rotation.eulerAngles.y >= 170 && transform.rotation.eulerAngles.z == 0)||
            (transform.rotation.eulerAngles.z >= 170 && transform.rotation.eulerAngles.y == 0))
        {
             transform.position = transform.position + new Vector3(0.005f * Speed, 0, 0);
        }
        else
        {
            transform.position = transform.position - new Vector3(0.005f * Speed, 0, 0);
        }
        GameObject ultraplex = GameObject.Find("ultraplex");
        if (ultraplex != null)
        {
            if (NextFire)
            {
                FireLaser();
                NextFire = false;
            }
        }
    }

    private void FireLaser()
    {
        GameObject laser;
        if (transform.rotation.eulerAngles.z >= 170)
        {
            laser = Instantiate(Spawnee, transform.position, Quaternion.Euler(0, 0, 180));
        }
        else
        {
            laser = Instantiate(Spawnee, transform.position, new Quaternion(0, 0, 0, 0));
        }
        laser.name = string.Format("laser");
        laser.transform.SetParent(transform);
    }
}
