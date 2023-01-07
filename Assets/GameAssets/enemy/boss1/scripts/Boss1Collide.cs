using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Collide : MonoBehaviour
{
    CapsuleCollider2D enemyHitbox;
    CapsuleCollider2D playerHitbox;

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        enemyHitbox = GetComponent<CapsuleCollider2D>();
        playerHitbox = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider2D>();

        damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<HealthManager>())
            {
                collision.gameObject.GetComponent<HealthManager>().Damage(damage);
                //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-30.0f, ForceMode2D.Impulse);
            }
        }
        if (collision.gameObject.CompareTag(gameObject.tag) || collision.gameObject.CompareTag("Untagged"))
        {

        }
    }
}
