using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    Rigidbody2D body;
    public Animator animator;
    Collider2D hitbox;

    Transform player;

    public GameObject eyeBulletPrefab;
    public Transform firePoint;

    public GameObject warning;
    public GameObject spike;

    public bool isFlipped = false;

    public HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        Physics2D.IgnoreCollision(hitbox, player.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void Update()
    {
        if(healthManager.healthPoints <= healthManager.healthBar.maxValue * 0.5)
        {
            animator.SetTrigger("Enrage");
        }
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

    public void Fire(int count, float spreadAngle)
    {
        if (count % 2 == 0) // If projectile count even
        {
            for (int i = 1; i <= count / 2; i++)
            {
                createEyeBullet((i * spreadAngle) - spreadAngle / 2);
                createEyeBullet(-((i * spreadAngle) - spreadAngle / 2));
            }
        }
        else // If projectile count odd
        {
            createEyeBullet(0);

            for (int i = 1; i <= (count - 1) / 2; i++)
            {
                createEyeBullet(i * spreadAngle);
                createEyeBullet(-i * spreadAngle);
            }
        }
    }

    public void StartShootSpike()
    {
        StartCoroutine(ShootSpike());
    }

    IEnumerator ShootSpike()
    {
        var targetPos = player.position;
        targetPos.y = -10;
        GameObject newWarning = Instantiate(warning, targetPos, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);
        GameObject newSpike = Instantiate(spike, targetPos, Quaternion.identity);
        newSpike.GetComponent<SpikeBehaviour>().ignoreTag = gameObject.tag;
        newSpike.GetComponent<SpikeBehaviour>().damageTag = "Player";
    }

    void createEyeBullet(float angle)
    {
        Vector2 playerPos = player.position;
        Vector2 currentPos = firePoint.position;

        Vector2 force = (Quaternion.AngleAxis(angle, Vector3.forward) * (playerPos - currentPos)).normalized;

        // To rotate bullet sprite
        float rotation = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;

        // Create Bullet
        GameObject newBullet = Instantiate(eyeBulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, rotation));
        newBullet.GetComponent<BulletBehaviour>().SetForce(force);
        newBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
        newBullet.GetComponent<BulletBehaviour>().damageTag = "Player";

        // Destroy after 2 seconds
        Destroy(newBullet, 5f);


        /*
        var newRotation = firePoint.rotation;
        newRotation *= Quaternion.Euler(0, 0, -90 + angle);
        GameObject eyeBullet = Instantiate(eyeBulletPrefab, firePoint.position, newRotation);

        Vector2 playerPos = player.transform.position;
        Vector2 currPos = transform.position;
        Vector2 force = (playerPos - currPos).normalized;

        eyeBullet.GetComponent<BulletBehaviour>().SetForce(RotateVector(force, angle));
        eyeBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
        eyeBullet.GetComponent<BulletBehaviour>().damageTag = "Player";

        Destroy(eyeBullet, 2f);
        */
    }

    Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}
