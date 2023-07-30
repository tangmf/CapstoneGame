using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    public GameObject logItem;
    // Start is called before the first frame update

    #region singleton
    public static LogManager instance;

    void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Log(string s)
    {
        Debug.Log("New log");
        GameObject logItemInstance = Instantiate(logItem, new Vector3(0, 0, 0), Quaternion.identity);
        logItemInstance.transform.parent = transform;

        logItemInstance.GetComponent<LogItem>().SetUp(s);

    }
}
