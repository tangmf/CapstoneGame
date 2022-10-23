using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb2d;
    public bool playerDetect = false;
    public GameObject entity;
    public Transform firePoint;
    private float nextActionTime = 0.0f;
    public float period = 1f;
    public GameObject bulletPrefab;

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

            GameObject newBullet = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

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
            newBullet.GetComponent<BulletBehaviour>().SetForce(force);
            newBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
            newBullet.GetComponent<BulletBehaviour>().damageTag = "Player";
            Destroy(newBullet, 2f);


            nextActionTime = Time.time + period;
        }
        

    }
}
