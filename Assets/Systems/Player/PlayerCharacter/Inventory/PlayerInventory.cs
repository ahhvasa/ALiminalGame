using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public void Start()
    {
        CurrentID = 0;
    }
    public Player player;

    [SerializeField] private Item[] items;
    public Item CurrentItem
    {
        get
        {
            return items[CurrentID];
        }
        set
        {
            items[CurrentID] = value;
        }
    }
    [SerializeField] private int _currentId;
    public int CurrentID
    {
        get { return _currentId; } 
        set
        {
            int previousId = _currentId;

            _currentId = value;
            _currentId = Mathf.Abs(_currentId);
            if (_currentId >= items.Length) { _currentId = items.Length - 1; }

            if (_currentId != previousId)
            {
                RemoveItemFromHands(previousId);
                TakeItemInHands(_currentId);
            }

            OnSetSlot?.Invoke(value);

            void TakeItemInHands(int id)
            {
                OnTakeInHands?.Invoke(items == null ? null : items[id], id);
                items[id]?.OnTakeInHands();
            }
            void RemoveItemFromHands(int id)
            {
                OnRemoveFromHands?.Invoke(items == null ? null : items[id], id);
                items[id]?.OnRemoveFromHands();
            }
        }
    }


    public event Action<Item, int> OnPickUp;
    public event Action<Item, int> OnDrop;

    public event Action<Item, int> OnTakeInHands;
    public event Action<Item, int> OnRemoveFromHands;

    public event Action<int> OnSetSlot;

    public void PickUpItem(Item item)
    {
        if (CurrentItem != null)
        {
            DropItem();
        }

        OnPickUp?.Invoke(item, CurrentID);

        CurrentItem = item;
        CurrentItem.OnPickUp(player);
    }

    public void DropItem()
    {
        OnDrop?.Invoke(CurrentItem, CurrentID);

        CurrentItem.OnDrop();
        CurrentItem = null;
    }



    public void FixedUpdate()
    {
        CurrentItem?.ItemFixedUpdate();
    }
    public void Update()
    {
        CurrentItem?.ItemUpdate();

        if (InputProvider.Drop())
        {
            DropItem();
        }
        if (InputProvider.SelectItem_1())
        {
            CurrentID = 0;
        }
        if (InputProvider.SelectItem_2())
        {
            CurrentID = 1;
        }
        if (InputProvider.SelectItem_3())
        {
            CurrentID = 2;
        }
        if (InputProvider.SelectItem_4())
        {
            CurrentID = 3;
        }
    }

}
