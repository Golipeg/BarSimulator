using System.Collections.Generic;
using System.Linq;
using Orders;
using UnityEngine;

public class BarItemsSlot : MonoBehaviour
{
    public bool IsAvailable => _items.Count < _maxSlotCount;
    public bool IsEmpty => _items.Count == 0;
    public ItemType ItemType = ItemType.Empty;
    [SerializeField] private int _maxSlotCount;

    private readonly Stack<Item> _items = new Stack<Item>();


    public void AddItem(Item item)
    {
        _items.Push(item);
        ItemType = item.ItemType;
        item.GiveItemForHolders(transform);
    }

    public bool TryGetItem(out Item item)
    {
        item = null;
        if (_items.Count > 0)
        {
            item = _items.Pop();
            if (_items.Count == 0)
            {
                ItemType = ItemType.Empty;
            }

            return true;
        }

        return false;
    }
}