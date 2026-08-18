using UnityEngine;
using System;

public class Item_AlarmClock : ItemHoldable
{
    public ObjectTextLabel objectTextLabel;

    private float currentTimeBeforeRing;

    private float currentTimeBeforeStartClock;
    [SerializeField] private float clockStartTimerTime = 2f;

    public Action OnStartCountdown;
    public Action OnEndCountdown;
    public Action OnCharge;
    public Action OnRing;

    public SoundPlayer soundPlayer;

    private bool clockActivated;

    public override void OnDropInternal()
    {
        base.OnDropInternal();
        isInHands = false;
    }
    public override void OnPickUpInternal(Player player)
    {
        base.OnPickUpInternal(player);
        isInHands = true;
    }  

    private void Update()
    {
        if (!clockActivated)
        {
            currentTimeBeforeStartClock -= Time.deltaTime;

            if (currentTimeBeforeStartClock <= 0 && currentTimeBeforeRing > 0f)
            {
                clockActivated = true;

                if (tickingSound == null)
                {
                    tickingSound = SoundManager.PlaySound(alarmClockTicking, soundPlayer);
                }

                OnStartCountdown?.Invoke();
            }

        }
        else
        {
            if (active == false && isInHands == true) { return; }

            currentTimeBeforeRing -= Time.deltaTime;

            if (currentTimeBeforeRing <= 0f)
            {
                currentTimeBeforeRing = 0f;
                UpdateLabel();

                clockActivated = false;

                if (tickingSound != null)
                {
                    tickingSound.DestroySound();
                    tickingSound = null;
                }

                OnEndCountdown?.Invoke();

                Ring();
                return;
            }

        }

        UpdateLabel();
    }


    public bool active;
    public bool isInHands = false;

    public override void ItemUpdateInternal()
    {
        if (active)
        {
            if (InputProvider.ActivateItem())
            {
                Charge();
            }
        }
    }

    private void Charge()
    {
        SoundManager.PlaySound(alarmClockWinding, soundPlayer);

        currentTimeBeforeRing += 1f;
        currentTimeBeforeStartClock = clockStartTimerTime;

        OnCharge?.Invoke();

        if (clockActivated)
        {
            clockActivated = false;
            OnEndCountdown?.Invoke();
        }

        UpdateLabel();
    }

    public override void Activate(bool activateOrDeactivate)
    {
        active = activateOrDeactivate;
    }

    private void Ring()
    {
        SoundManager.PlaySound(alarmClockRing, soundPlayer);
        OnRing?.Invoke();
    }

    private void UpdateLabel()
    {
        if (currentTimeBeforeRing <= 0.1f)
        {
            objectTextLabel.Text = "";
        }
        else
        {
            objectTextLabel.Text = currentTimeBeforeRing.ToString("F1");
        }
    }





    public SoundData alarmClockWinding;
    public SoundData alarmClockTicking;
    public SoundData alarmClockRing;

    private Sound tickingSound;

}