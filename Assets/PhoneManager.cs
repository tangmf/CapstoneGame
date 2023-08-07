using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public KeyCode key;
    public GameObject phone;
    public List<GameObject> apps;
    public GameObject currentApp;
    public Transform appContainer;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform app in appContainer){
            apps.Add(app.gameObject);
        }
        OpenApp(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (phone.activeSelf)
            {
                Phone(false);
            }
            else
            {
                Phone(true);
            }
        }
    }

    public void Phone(bool b)
    {
        if (b)
        {
            phone.SetActive(true);
        }
        else
        {
            phone.SetActive(false);
        }
    }

    public void OpenApp(int index)
    {
        foreach (GameObject go in apps)
        {
            CloseApp(go);
        }
        GameObject app = apps[index];
        currentApp = app;
        app.SetActive(true);
    }
    
    public void CloseApp(GameObject app)
    {
        app.SetActive(false);
        currentApp = null;
    }

}
