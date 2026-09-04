using UnityEngine;

public class ObjectSmell : MonoBehaviour, IPercivableObject
{
    public PerceivableObject perceivableObject;
    public bool AIIgnore;
    public float smellDistance = 10;

    public PerceivableObject PerceivableObject
    {  
        get { return perceivableObject; }
    }

    public void Awake()
    {
        if (perceivableObject == null)
        {
            if (gameObject.TryGetComponent<PerceivableObject>(out perceivableObject) == false)
            {
                perceivableObject = gameObject.AddComponent<PerceivableObject>();
            }
        }
        perceivableObject.objectSmell = this;
    }

    public void OnEnable()
    {
        if (ObjectSmellManager.Instance == null) { return; }
        ObjectSmellManager.Instance.objectSmell.Add(this);
    }
    public void OnDisable()
    {
        if (ObjectSmellManager.Instance == null) { return; }
        ObjectSmellManager.Instance.objectSmell.Remove(this);
    }
}
