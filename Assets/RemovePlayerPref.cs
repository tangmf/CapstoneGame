using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerPref : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkFile;
    public bool remove = true;
    // Start is called before the first frame update
    void Start()
    {
        if (remove)
        {
            PlayerPrefs.SetString(inkFile.name, "Replay");
        }
        else
        {
            PlayerPrefs.SetString(inkFile.name, "Done");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
