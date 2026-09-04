using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;

    public GameObject note;
    public Text textObject;

    public void Awake()
    {
        Instance = this;
    }

    public async void ShowNote(LocalizedString stringReference)
    {
        note.SetActive(true);

        string text = await stringReference.GetLocalizedStringAsync().Task;
        textObject.text = text;
    }

    public void HideNote()
    {
        note.SetActive(false);
    }
}