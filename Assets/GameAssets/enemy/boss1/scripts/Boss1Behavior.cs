using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behavior : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;
    Collider2D hitbox;

    Transform player;

    public GameObject eyeBulletPrefab;
    public Transform firePoint;

    public bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitbox = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        Physics2D.IgnoreCollision(hitbox, player.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Fire()
    {
        createEyeBullet(-30);
        createEyeBullet(-10);
        createEyeBullet(10);
        createEyeBullet(30);

        void createEyeBullet(float angle)
        {
            GameObject eyeBullet = Instantiate(eyeBulletPrefab, firePoint.position, firePoint.rotation);

            Vector2 playerPos = player.transform.position;
            Vector2 currPos = transform.position;
            Vector2 force = (playerPos - currPos).normalized;

            eyeBullet.GetComponent<EyeBulletBehavior>().SetForce(RotateVector(force, angle));
            eyeBullet.GetComponent<EyeBulletBehavior>().ignoreTag = gameObject.tag;
            eyeBullet.GetComponent<EyeBulletBehavior>().damageTag = "Player";

            Destroy(eyeBullet, 2f);
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
