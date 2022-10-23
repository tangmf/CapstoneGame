using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float delay = 0f;
    public AudioClip audio;

    // Use this for initialization
    void Start()
    {
        if (audio != null)
        {
            AudioSource.PlayClipAtPoint(audio, this.gameObject.transform.position);
        }
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
