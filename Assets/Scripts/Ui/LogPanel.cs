using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogPanel : MonoBehaviour
{
    protected static LogPanel current;

    public Text logLabel;

    private void Awake()
    {
        current = this;
    }

    public static void write(string message)
    {
        current.logLabel.text = message;
    }
}
