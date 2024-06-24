using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class LogPanel : MonoBehaviour
{
    protected static LogPanel current;
    public TMP_Text logLabel;
    // Start is called before the first frame update
    void Awake()
    {
        current=this;
    }

    // Update is called once per frame
    public static void write(string message)
    {
        current.logLabel.text = message;
    }
}
