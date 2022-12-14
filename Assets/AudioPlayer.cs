using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    AudioSource audioSource;
    private static AudioPlayer instance;

    Scene firstScene;
    Scene newScene;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        firstScene = SceneManager.GetActiveScene();
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        newScene = SceneManager.GetActiveScene();
        if(newScene.name[0] == 'L' && newScene.name != "LevelSelect")
        {
            audioSource.Stop();
        }
        else if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

}
