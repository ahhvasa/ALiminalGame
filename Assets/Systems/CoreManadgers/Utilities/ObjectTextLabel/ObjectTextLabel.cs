using MyLibrary.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTextLabel : MonoBehaviour
{
    [SerializeField] private string text;
    private ObjectTextItem textItem;

    public string Text
    {
        set
        {
            text = value;
            textItem.SetText(text);
        }
        get { return text; }
    }
    void Start()
    {
        textItem = ObjectTextManadger.Instance.Get();
        textItem.transform.position = transform.position;
        textItem.transform.SetParent(transform);
        Text = text;
    }
}
