using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;

    public GameObject entity;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public bool playerDetect = false;

    private float nextActionTime = 0.0f;
    public float period = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = entity.gameObject.GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = entity.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            //animator.SetBool("PlayerDetect", false);
            playerDetect = false;
        }

    }

    void Shoot()
    {
        if (Time.time > nextActionTime)
        {
            createBullet(0);
            createBullet(-20);
            createBullet(-40);
            createBullet(20);
            createBullet(40);

            nextActionTime = Time.time + period;
        }

        void createBullet(float angle)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            GameObject player = GameObject.FindWithTag("Player");
            Vector2 playerPos = player.transform.position;
            Vector2 currPos = transform.position;
            Vector2 force = (playerPos - currPos).normalized;

            if (force.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (force.x < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

            bullet.GetComponent<BulletBehaviour>().SetForce(RotateVector(force, angle));
            bullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
            bullet.GetComponent<BulletBehaviour>().damageTag = "Player";

            Destroy(bullet, 2f);
        }

        Vector2 RotateVector(Vector2 v, float angle)
        {
            float radian = angle * Mathf.Deg2Rad;
            float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
            float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
            return new Vector2(_x, _y);
        }
    }
}