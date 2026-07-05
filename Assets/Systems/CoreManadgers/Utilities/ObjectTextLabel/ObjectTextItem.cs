using UnityEngine;

public class ObjectTextItem : MonoBehaviour
{
    public TextMesh textMesh;
    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
