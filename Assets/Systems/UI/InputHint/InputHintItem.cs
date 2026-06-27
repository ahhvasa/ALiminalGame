using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InputHintItem : MonoBehaviour
{
    [SerializeField] private Text keyText;
    [SerializeField] private Text commandText;

    public void SetText(InputHintInfo hint)
    {
        keyText.text = hint.key;
        commandText.text = hint.command;
    }
}

[Serializable]
public class InputHintInfo
{
    public string key;
    public string command;

    public InputHintInfo(string key, string command)
    {
        this.key = key;
        this.command = command;
    }
}
