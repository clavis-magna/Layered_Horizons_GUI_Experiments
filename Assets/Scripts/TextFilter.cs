using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFilter : MonoBehaviour
{


    Dictionary<string, string> debugLogs = new Dictionary<string, string>();

    public Text display;
    public string filter;

    //private void Update()
    //{
    //    //Debug.Log("time:" + Time.time);
    //    //Debug.Log(gameObject.name);
    //}

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    //collect all Debug.Logs
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log)
        {
            //split the string at the colon
            string[] splitString = logString.Split(char.Parse(":"));
            string debugKey = splitString[0];
            string debugValue = splitString.Length > 1 ? splitString[1] : "";

            if (debugKey.Contains(filter))
            {
                debugLogs[debugKey] = debugValue;
            }
        }

        string displayText = "";
        foreach (KeyValuePair<string, string> log in debugLogs)
        {
            if (log.Value == "")
            {
            }
            else
            {
                displayText = log.Key + ": " + log.Value;
            }
        }
        display.text = displayText;
    }
}
