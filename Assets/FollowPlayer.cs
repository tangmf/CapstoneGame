using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float nextAttackTime;
    public float coolDown;
    public Transform firepoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public float moveSpeed = 5.0f;
    public float damage = 10.0f;
    GameObject playerRef;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = Vector2.MoveTowards(transform.position, playerRef.transform.position, moveSpeed * Time.deltaTime);

        if (playerRef.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (!animator.GetBool("Detected"))
        {
            Vector2 moveTowardsPos = new Vector2(playerRef.transform.position.x, 0);
            transform.position = Vector2.MoveTowards(transform.position, moveTowardsPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (Time.time >= nextAttackTime)
            {

                Debug.Log("ATTACK");
                animator.SetBool("Attack", true);



                nextAttackTime = Time.time + coolDown;
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }





    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(firepoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Player") && !enemy.CompareTag("Ground"))
            {
                if (enemy.gameObject.GetComponent<HealthManager>())
                {
                    enemy.gameObject.GetComponent<HealthManager>().Damage(damage);
                }

            }

        }
    }
}
