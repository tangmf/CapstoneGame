using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;

    public float cooldown;
    public float attackTime;
    public string direction = "LEFT";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (attackTime + cooldown < Time.time)
        {
            Shoot();

            attackTime = Time.time;
        }
        */
    }

    public void ShootBullet()
    {
        // Create Bullet
        GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, 0f));
        if (direction == "LEFT")
        {
            newBullet.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            newBullet.GetComponent<BulletBehaviour>().SetForce(new Vector2(-1, 0), 30);
        }
        else
        {
            newBullet.GetComponent<BulletBehaviour>().SetForce(new Vector2(1, 0), 30);
        }
        
        newBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
        newBullet.GetComponent<BulletBehaviour>().damageTag = "Player";

        // Destroy after 5 seconds
        Destroy(newBullet, 5f);
    }
}
