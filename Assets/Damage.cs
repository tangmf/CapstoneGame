using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public string damageTag;
    public int damageAmt = 10;
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

        if (collision.gameObject.CompareTag(damageTag))
        {
            if (collision.gameObject.GetComponent<HealthManager>())
            {
                collision.gameObject.GetComponent<HealthManager>().Damage(damageAmt);
                Destroy(gameObject);
            }
        }

        
    }
}
