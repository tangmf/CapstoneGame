using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerPref : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkFile;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString(inkFile.name, "Replay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
