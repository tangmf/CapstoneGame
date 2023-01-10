using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;

    public float cooldown;
    public float attackTime;
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
        newBullet.GetComponent<BulletBehaviour>().SetForce(new Vector2(1, 0));
        newBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
        newBullet.GetComponent<BulletBehaviour>().damageTag = "Player";

        // Destroy after 2 seconds
        Destroy(newBullet, 2f);
    }
}
