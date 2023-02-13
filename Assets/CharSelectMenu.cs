using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelectMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ToggleOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleOn()
    {
        gameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }
}
