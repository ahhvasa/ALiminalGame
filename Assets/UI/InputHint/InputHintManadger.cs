using System.Collections.Generic;
using UnityEngine;

public class InputHintManadger : MonoBehaviour
{
    public static InputHintManadger Instance;
    public InputHintItem prefab;
    public ObjectPull<InputHintItem> objectPull;
    public Transform context;
    private Dictionary<InputHintInfo, InputHintItem> items = new();

    public void Awake()
    {
        Instance = this;
        objectPull = new ObjectPull<InputHintItem>(prefab, 10);
    }

    public void ShowHint(InputHintInfo hint)
    {
        if (items.ContainsKey(hint)) { items[hint].SetText(hint);  return; }

        items.Add(hint, GetItem());
        items[hint].SetText(hint);
    }
    public void RemoveHint(InputHintInfo hint)
    {
        objectPull.ReturnObject(items[hint]);
        items.Remove(hint);
    }


    public InputHintItem GetItem()
    {
        InputHintItem item = objectPull.GetObject();
        item.transform.SetParent(context.transform);
        return item;
    }

}
