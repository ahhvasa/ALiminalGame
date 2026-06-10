using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
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

            void TakeItemInHands(int id)
            {
                items[id]?.OnTakeInHands();
            }
            void RemoveItemFromHands(int id)
            {
                items[id]?.OnRemoveFromHands();
            }
        }
    }

    public void PickUpItem(Item item)
    {
        if (CurrentItem != null)
        {
            DropItem();
        }
        CurrentItem = item;
        CurrentItem.OnPickUp(player);
    }

    public void DropItem()
    {
        CurrentItem.OnDrop();
        CurrentItem = null;
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentID = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentID = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CurrentID = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CurrentID = 3;
        }
    }

}
