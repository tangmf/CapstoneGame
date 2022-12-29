using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accursed_anathema_collide : MonoBehaviour
{
    CapsuleCollider2D enemyHitbox;
    CapsuleCollider2D playerHitbox;

    public float damage;

    public string ignoreTag;
    public string damageTag;

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
        if (collision.gameObject.CompareTag(damageTag))
        {
            if (collision.gameObject.GetComponent<HealthManager>())
            {
                collision.gameObject.GetComponent<HealthManager>().Damage(damage);
            }
        }
        if (collision.gameObject.CompareTag(ignoreTag) || collision.gameObject.CompareTag("Untagged"))
        {

        }
    }
}
