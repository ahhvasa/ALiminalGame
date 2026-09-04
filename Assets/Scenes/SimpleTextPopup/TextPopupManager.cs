using UnityEngine;

public class TextPopupManager : MonoBehaviour
{
    public static TextPopupManager Instance;

    private Player player;            
    public ObjectPull<TextFade> objectPull;
    public TextFade prefab;

    public void Awake()
    {
        Instance = this;
        player = FindObjectOfType<Player>();
        objectPull = new ObjectPull<TextFade>(prefab, 2);
    }

    public void ShowTextOnPlayer(string text)
    {
        TextFade textObject = GetTemporaryText(text);
        textObject.transform.position = player.transform.position;
    }

    public TextFade GetTemporaryText(string text)
    {
        TextFade instance = objectPull.GetObject();
        instance.text.text = text;
        instance.OnFadeFinish += () => objectPull.ReturnObject(instance);

        return instance;
    }


    public void Update()
    {

    }
}