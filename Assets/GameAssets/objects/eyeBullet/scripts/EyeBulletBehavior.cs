using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBulletBehavior : MonoBehaviour
{
    public EyeBullet eyeBullet;

    Collider2D hitbox;
    BoxCollider2D foreground;

    public string ignoreTag;
    public string damageTag;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<Collider2D>();

        foreground = GameObject.FindGameObjectWithTag("Foreground").GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(hitbox, foreground);

        /*if (eyeBullet.shootSfx != null)
        {
            AudioSource.PlayClipAtPoint(eyeBullet.shootSfx, transform.position);
        }*/
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
                collision.gameObject.GetComponent<HealthManager>().Damage(eyeBullet.damage);
            }
        }
        if (collision.gameObject.CompareTag(ignoreTag) || collision.gameObject.CompareTag("Untagged"))
        {

        }
        else
        {
            GameObject effect = Instantiate(eyeBullet.hitEffect, transform.position, transform.rotation);
            if (eyeBullet.hitSfx != null)
            {
                //AudioSource.PlayClipAtPoint(eyeBullet.hitSfx, transform.position);
            }
            effect.layer = gameObject.layer;
            effect.GetComponent<SpriteRenderer>().sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
            Destroy(gameObject);
        }
    }

    public void SetForce(Vector2 force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(force * eyeBullet.forceMultiplier, ForceMode2D.Impulse);
    }
}
