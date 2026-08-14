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
    [Inject] ISaveSystem saveSystem;
    public GameObject exclamationMark;

    public LocalizedString stringReference;
    public string noteKey;
    public bool noExclamationMark = false;

    bool active;
    bool on = false;

    public async Task Start()
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
            NoteManadger.Instance.ShowNote(stringReference);
        }
        else
        {
            NoteManadger.Instance.HideNote();
        }
    }

    public async Task WasReadNow(bool wasRead)
    {
        await saveSystem.SaveAsync<String_vv111>(noteKey, wasRead ? new String_vv111("1") : new String_vv111("0"));
    }

    public async Task<bool> WasReadEarlier()
    {
        if (noExclamationMark) { return true; }

        string value = (await saveSystem.LoadAsync<String_vv111>(noteKey)).str;

        return value == "1";
    }

}

[Serializable]
public class String_vv111
{
    public string str;

    public String_vv111(string str)
    {
        this.str = str;
    }
}
