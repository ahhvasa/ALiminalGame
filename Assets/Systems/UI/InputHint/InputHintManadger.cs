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
        objectPull = new ObjectPull<InputHintItem>(prefab, 10, context);
    }

    public static void ShowHint(InputHintInfo hint)
    {
        if (Instance.items.ContainsKey(hint)) { Instance.items[hint].SetText(hint);  return; }

        Instance.items.Add(hint, Instance.GetItem());
        Instance.items[hint].SetText(hint);
    }
    public static void RemoveHint(InputHintInfo hint)
    {
        if (Instance.items.ContainsKey(hint) == false) { return; }

        Instance.objectPull.ReturnObject(Instance.items[hint]);
        Instance.items.Remove(hint);
    }


    public InputHintItem GetItem()
    {
        InputHintItem item = objectPull.GetObject();
        item.transform.SetParent(context.transform);
        return item;
    }

}
