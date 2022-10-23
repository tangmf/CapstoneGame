using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 currPos = transform.position;
            Vector2 force = (mousePos - currPos).normalized;
            if (force.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (force.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            float rotation = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;

            // Create Bullet
            GameObject newBullet = (GameObject)Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, rotation));
            newBullet.GetComponent<BulletBehaviour>().SetForce(force);
            newBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
            newBullet.GetComponent<BulletBehaviour>().damageTag = "Enemy";
            // Destroy after 2 seconds
            Destroy(newBullet, 2f);
        }
    }
}
