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
            if (textItem == null) { return; }

            textItem.SetText(text);
        }
        get { return text; }
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        if (ObjectTextManadger.Instance == null) { return; }
        if (textItem != null) { return; }

        textItem = ObjectTextManadger.Instance.Get();
        textItem.transform.position = transform.position;
        textItem.transform.SetParent(transform);
        Text = text;
    }

    private void OnDisable()
    {
        if (textItem == null) { return; }

        ObjectTextManadger.Instance.Return(textItem);
        textItem = null;
    }
}
