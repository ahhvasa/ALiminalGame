using UnityEngine;

public class ObjectTextManager : MonoBehaviour
{
    public static ObjectTextManager Instance;
    public ObjectPull<ObjectTextItem> objectPull;
    public ObjectTextItem prefab;

    public void Awake()
    {
        Instance = this;
        objectPull = new ObjectPull<ObjectTextItem>(prefab);
    }

    public ObjectTextItem Get()
    {
        return objectPull.GetObject();
    }

    public void Return(ObjectTextItem textItem)
    {
        objectPull.ReturnObject(textItem);
    }
}
