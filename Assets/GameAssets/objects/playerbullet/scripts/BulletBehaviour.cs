using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Bullet bullet;
    public string ignoreTag;
    public string damageTag;
    public bool destroyOnImpact = true;

    public Transform bulletTransform;

    // Start is called before the first frame update
    void Start()
    {
        bulletTransform = GetComponent<Transform>();

        if (bullet.shootSfx != null)
        {
            AudioSource.PlayClipAtPoint(bullet.shootSfx, transform.position);
        }
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
                collision.gameObject.GetComponent<HealthManager>().Damage(bullet.damage);
                if (destroyOnImpact)
                {
                    Debug.Log("Bullet hit " + collision.gameObject.ToString());
                    GameObject effect = Instantiate(bullet.hitEffect, transform.position, transform.rotation);
                    if (bullet.hitSfx != null)
                    {

                        AudioSource.PlayClipAtPoint(bullet.hitSfx, transform.position);

                    }
                    effect.layer = gameObject.layer;
                    effect.GetComponent<SpriteRenderer>().sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
                    Destroy(gameObject);
                }
            }
        }
        
        else if (collision.gameObject.CompareTag(ignoreTag) || collision.gameObject.CompareTag("Untagged"))
        {

        }
        else
        {
            if (destroyOnImpact)
            {
                //Debug.Log("Bullet hit " + collision.gameObject.ToString());
                GameObject effect = Instantiate(bullet.hitEffect, transform.position, transform.rotation);
                if (bullet.hitSfx != null)
                {

                    AudioSource.PlayClipAtPoint(bullet.hitSfx, transform.position);

                }
                effect.layer = gameObject.layer;
                effect.GetComponent<SpriteRenderer>().sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
                Destroy(gameObject);
            }
        }


    }

    public void SetForce(Vector2 force)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody2D>().AddForce(force * bullet.bulletSpeed, ForceMode2D.Impulse);
    }
    

}
