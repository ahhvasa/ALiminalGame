using MyLibrary.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTextLabel : MonoBehaviour
{
    public string text;
    void Start()
    {
        ObjectTextItem textItem = ObjectTextManadger.Instance.Get();
        textItem.transform.position = transform.position;
        textItem.transform.SetParent(transform);
        textItem.SetText(text);
    }
}
