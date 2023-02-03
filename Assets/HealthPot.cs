using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : MonoBehaviour
{
    public AudioClip hitSfx;
    public float amt;
    public GameObject effect;
    public string healTag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(healTag))
        {
            if (collision.gameObject.GetComponent<HealthManager>())
            {
                if (!collision.gameObject.GetComponent<HealthManager>().dead && collision.gameObject.GetComponent<HealthManager>().healthPoints < collision.gameObject.GetComponent<HealthManager>().healthBar.maxValue)
                {
                    if(effect != null)
                    {
                       Instantiate(effect, transform.position, transform.rotation);
                    }
                    
                    if (hitSfx != null)
                    {

                        AudioSource.PlayClipAtPoint(hitSfx, transform.position);

                    }
                    collision.gameObject.GetComponent<HealthManager>().Heal(amt);
                    Destroy(gameObject);
                }
            }
        }


    }
}
