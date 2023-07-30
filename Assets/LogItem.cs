using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogItem : MonoBehaviour
{
    public TMP_Text description;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(string details)
    {
        description.text = details;
    }
}
