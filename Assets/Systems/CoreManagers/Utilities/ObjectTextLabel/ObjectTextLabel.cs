using MyLibrary.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTextLabel : MonoBehaviour
{
    [SerializeField] private string text;
    private ObjectTextItem textItem;
    private VisibleObject visibleObject;
    public bool overrideFontSize;
    public float fontSize;

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
        visibleObject = GetComponentInParent<VisibleObject>();
    }

    public void FixedUpdate()
    {
        TrySetLabel();
    }

    private void OnEnable()
    {
        TrySetLabel();
    }

    public void TrySetLabel()
    {
        if (textItem != null) { return; }
        if (ObjectTextManager.Instance == null) { return; }
        if (visibleObject == null) { visibleObject = GetComponentInParent<VisibleObject>();  }

        textItem = ObjectTextManager.Instance.Get();
        textItem.transform.position = transform.position;
        textItem.transform.SetParent(transform);

        if (overrideFontSize)
        {
            textItem.textMesh.fontSize = fontSize;
        }

        Text = text;

        visibleObject.ConnectObject(textItem.gameObject);
    }

    private void OnDisable()
    {
        if (textItem == null) { return; }

        visibleObject.DisconnectObject(textItem.gameObject);
        ObjectTextManager.Instance.Return(textItem);
        textItem = null;

    }
}
