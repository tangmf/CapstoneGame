using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform pivot;
    public Transform firePoint;

    public float cooldown;
    public float attackTime;
    public float bulletSpeed;

    public float attackRange = 1.0f;
    public float damage = 10.0f;
    public AudioClip meleeSfx;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPos = transform.position;
        Vector2 force = (mousePos - currentPos).normalized;


        float rotation = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        pivot.rotation= Quaternion.Euler(0f, 0f, rotation);

        if (Input.GetMouseButtonDown(0))
        {
            if (attackTime + cooldown < Time.time)
            {



                // Create Bullet
                GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, rotation));
                newBullet.GetComponent<BulletBehaviour>().SetForce(force * bulletSpeed);
                newBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
                newBullet.GetComponent<BulletBehaviour>().damageTag = "Enemy";

                // Destroy after 2 seconds
                Destroy(newBullet, 2f);

                attackTime = Time.time;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (attackTime + cooldown < Time.time)
            {
                animator.SetTrigger("Attack");
            }

        }
    }

    public void MeleeAttack()
    {
        if (meleeSfx != null)
        {
            AudioSource.PlayClipAtPoint(meleeSfx, this.gameObject.transform.position);
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPos = firePoint.position;
        Vector2 force = (mousePos - currentPos).normalized;

        if (force.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (force.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        float rotation = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firePoint.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {

            /*
            if (!enemy.CompareTag("Player") && enemy.gameObject.GetComponent<Rigidbody2D>())
            {
                enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 10.0f, ForceMode2D.Impulse);

                //Destroy(enemy.gameObject);
            }
            */
            if (!enemy.CompareTag("Player") && !enemy.CompareTag("Ground"))
            {
                Debug.Log("MELEE HIT: " + enemy.gameObject.ToString());
                if (enemy.gameObject.GetComponent<HealthManager>())
                {
                    enemy.gameObject.GetComponent<HealthManager>().Damage(damage);
                }

                if (enemy.gameObject.GetComponent<BulletBehaviour>() && enemy.gameObject.GetComponent<Rigidbody2D>())
                {
                    enemy.transform.position = firePoint.position;
                    enemy.gameObject.GetComponent<BulletBehaviour>().SetForce(force);
                    //enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 1.0f, ForceMode2D.Impulse);
                    enemy.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
                    enemy.gameObject.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
                    enemy.gameObject.GetComponent<BulletBehaviour>().damageTag = "Enemy";
                }

                if (enemy.gameObject.GetComponent<Rigidbody2D>() && !enemy.gameObject.GetComponent<BulletBehaviour>())
                {

                    enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 10.0f, ForceMode2D.Impulse);
                }
            }



        }

        attackTime = Time.time;
        animator.ResetTrigger("Attack");

    }
    
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(firePoint.position, attackRange);
    }
    
    
}
