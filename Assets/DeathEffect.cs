using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    public GameObject effect;
    public ParticleSystem particle;
    public AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        if (effect != null)
        {
            Instantiate(effect, transform.position, transform.rotation);
        }
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
        }
        if(particle != null)
        {
            var newParticle = Instantiate(particle, transform.position, Quaternion.identity);
            newParticle.Play();
            Destroy(newParticle, 2f);
        }

    }
}
