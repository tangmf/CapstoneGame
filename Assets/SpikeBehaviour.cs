using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{

    public Bullet bullet;
    public string ignoreTag;
    public string damageTag;

    // Start is called before the first frame update
    void Start()
    {
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
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(1,1,0) * 50.0f, ForceMode2D.Impulse);
                if(bullet.hitSfx != null)
                {
                    AudioSource.PlayClipAtPoint(bullet.hitSfx, transform.position);
                }
                
            }
        }

    }

    /*
    void OnTriggerStay2D(Collider2D other)
    {
        other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);
    }
    */

    public void SetForce(Vector2 force)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(force * bullet.forceMultiplier, ForceMode2D.Impulse);
    }


}
