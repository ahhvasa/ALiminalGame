using UnityEngine;

public class ObjectSmell : MonoBehaviour
{
    public PerceivableObject perceivableObject;
    public bool AIIgnore;

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
        ObjectSmellManadger.Instance.objectSmell.Add(this);
    }
    public void OnDisable()
    {
        ObjectSmellManadger.Instance.objectSmell.Remove(this);
    }
}
