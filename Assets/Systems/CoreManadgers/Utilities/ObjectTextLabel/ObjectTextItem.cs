using UnityEngine;

public class ObjectTextItem : MonoBehaviour
{
    public Player player;

    public float minDistance = 2f; 
    public float maxDistance = 6f; 

    public void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public TextMesh textMesh;
    public void SetText(string text)
    {
        textMesh.text = text;
    }

    public void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        float alpha = 1f - Mathf.InverseLerp(minDistance, maxDistance, distance);

        Color color = textMesh.color;
        color.a = alpha;
        textMesh.color = color;
    }
}
