using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LogData
{
    public string logContent;
    public string dateTime;
    public string source;

    public LogData(string lc, string dt, string s)
    {
        logContent = lc;
        dateTime = dt;
        source = s;
    }

}
