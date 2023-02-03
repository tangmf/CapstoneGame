using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform pivot;
    public Transform firePoint;

    public float cooldown;
    public float attackTime;

    public float attackRange = 1.0f;
    public float damage = 10.0f;
    public float swingForce = 2f;
    public AudioClip meleeSfx;

    public GameObject abilityBullet;
    public float nextShootTime = 0.0f;
    public float abilityCD = 10.0f;
    public Image abilityCDImage;
    public TextMeshProUGUI abilityCDText;

    public Animator animator;
    Rigidbody2D rb;

    CapsuleCollider2D collider;
    Vector2 normalSize;
    Vector2 normalOffset;
    Vector2 newSize;
    Vector2 newOffset;

    float normalSpeed;
    float newSpeed;

    public bool lockDirection = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        collider = GetComponent<CapsuleCollider2D>();
        normalSize = collider.size;
        newSize = collider.size * new Vector2(1f, 0.5f);


        normalOffset = collider.offset;
        newOffset = collider.offset * new Vector2(1f, 0.5f);

        normalSpeed = GetComponent<PlayerMovement>().playerMoveSpeed;
        newSpeed = normalSpeed * 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (nextShootTime + abilityCD < Time.timeSinceLevelLoad)
        {
            abilityCDImage.fillAmount = 0;
            abilityCDText.text = "0";
        }
        else
        {
            int timer = (int)(nextShootTime - Time.timeSinceLevelLoad) + (int)(abilityCD);
            abilityCDImage.fillAmount = timer /abilityCD;
            abilityCDText.text = timer.ToString();
        }
            
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPos = transform.position;
        Vector2 force = (mousePos - currentPos).normalized;


        float rotation = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(0f, 0f, rotation);

        if (Input.GetMouseButtonDown(0))
        {
            if (attackTime + cooldown < Time.time)
            {
                
                animator.SetTrigger("Attack");
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            if (nextShootTime + abilityCD < Time.timeSinceLevelLoad)
            {
                Ability1();
            }

        }

    }

    public void RotateToAttackPoint()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPos = transform.position;
        Vector2 force = (mousePos - currentPos).normalized;
        Debug.Log("POS1" + firePoint.position.x + " , " + firePoint.position.y);

        if (force.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (force.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        LockDirection();
    }

    public void MeleeAttack()
    {
        rb.AddForce(transform.right * swingForce, ForceMode2D.Force);
        if (meleeSfx != null)
        {
            AudioSource.PlayClipAtPoint(meleeSfx, this.gameObject.transform.position);
        }



        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPos = firePoint.position;

        Vector2 force = (mousePos - currentPos).normalized;

        Debug.Log("POS2" + firePoint.position.x + " , " + firePoint.position.y);
        float rotation = Mathf.Atan2(force.y, force.x) * Mathf.Rad2Deg;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(currentPos, attackRange);
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
                    enemy.transform.position = currentPos;
                    //force.y += prevBulletTransform.rotation.eulerAngles.z;
                    enemy.gameObject.GetComponent<BulletBehaviour>().SetForce(force * 2);
                    //enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 1.0f, ForceMode2D.Impulse);
                    enemy.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
                    enemy.gameObject.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
                    enemy.gameObject.GetComponent<BulletBehaviour>().damageTag = "Enemy";
                }

                if (enemy.gameObject.GetComponent<Rigidbody2D>() && !enemy.gameObject.GetComponent<BulletBehaviour>())
                {

                    enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(force * 50.0f, ForceMode2D.Impulse);
                }
            }



        }

        attackTime = Time.time;
        animator.ResetTrigger("Attack");

    }

    public void Ability1()
    {
        rb.AddForce(-transform.right * swingForce*2, ForceMode2D.Force);
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

        // Create Bullet
        GameObject newBullet = Instantiate(abilityBullet, firePoint.position, Quaternion.Euler(0f, 0f, rotation));
        newBullet.GetComponent<BulletBehaviour>().SetForce(force);
        newBullet.GetComponent<BulletBehaviour>().ignoreTag = gameObject.tag;
        newBullet.GetComponent<BulletBehaviour>().damageTag = "Enemy";

        // Destroy after 5 seconds
        Destroy(newBullet, 5f);

        nextShootTime = Time.timeSinceLevelLoad;

    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(firePoint.position, attackRange);
    }
    

    public void Crouch()
    {
        collider.size = newSize;
        collider.offset = newOffset;
        GetComponent<PlayerMovement>().playerMoveSpeed = newSpeed;
    }

    public void UnCrouch()
    {
        collider.size = normalSize;
        collider.offset = normalOffset;
        GetComponent<PlayerMovement>().playerMoveSpeed = normalSpeed;
    }

    IEnumerator LockDirectionEnumerator()
    {
        lockDirection = true;
        yield return new WaitForSeconds(cooldown);
        lockDirection = false;
    }

    public void LockDirection()
    {
        lockDirection = true;
    }

    public void UnLockDirection()
    {
        lockDirection = false;
    }

}
