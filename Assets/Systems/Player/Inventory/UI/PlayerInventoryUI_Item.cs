using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI_Item : MonoBehaviour
{
    public Sprite CurrentSprite
    {
        get
        {
            return image.sprite;
        }
        set
        {
            if (value == null)
            {
                image.enabled = false;
            }
            else
            {
                image.enabled = true;
            }

            image.sprite = value;
        }
    }
    [SerializeField] public Image image;
    public GameObject activeBorder;
}
