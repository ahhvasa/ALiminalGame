using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using Zenject;
using System.Threading.Tasks;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using System;


public class NoteObject : ItemHoldable
{
    public GameObject exclamationMark;

    public LocalizedString stringReference;
    public string noteKey;
    public bool noExclamationMark = false;

    bool active;
    bool on = false;

    public new async Task Start()
    {
        base.Start();
        ShowExclamationMark(await WasReadEarlier() == false);
    }

    public void ShowExclamationMark(bool showOrHide)
    {
        exclamationMark.SetActive(showOrHide);
    }

    public override void Activate(bool activateOrDeactivate)
    {
        if (on)
        {
            Show();
        }

        active = activateOrDeactivate;
    }

    public override void ItemUpdateInternal()
    {
        if (active)
        {
            if (InputProvider.ActivateItem())
            {
                Show();
            }
        }
    }

    public void Show()
    {
        if (exclamationMark.activeSelf == true)
        {
            WasReadNow(true);
            ShowExclamationMark(false);
        }

        on = !on;

        if (on)
        {
            NoteManager.Instance.ShowNote(stringReference);
        }
        else
        {
            NoteManager.Instance.HideNote();
        }
    }

    public async Task WasReadNow(bool wasRead)
    {
        await PlayerPrefsSaveSystem_StaticSingleton.Instance.SaveAsync<SerializableString>(noteKey, wasRead ? new SerializableString("1") : new SerializableString("0"));
    }

    public async Task<bool> WasReadEarlier()
    {
        if (noExclamationMark) { return true; }

        string value = (await PlayerPrefsSaveSystem_StaticSingleton.Instance.LoadAsync<SerializableString>(noteKey)).str;

        return value == "1";
    }

}

[Serializable]
public class SerializableString
{
    public string str;

    public SerializableString(string str)
    {
        this.str = str;
    }
}
