using System;
using System.Collections.Generic;
using Orders;
using UnityEngine;

namespace DefaultNamespace
{
    public class BarManager : MonoBehaviour
    {
        [SerializeField] private List<BarItemsSlot> ItemsSlots;


        public bool TryAddItemToBar(Item item)
        {
            var freeBarSlot = FindFreeSlot(item.ItemType);
            if (freeBarSlot != null)
            {
                freeBarSlot.AddItem(item);
                return true;
            }

            return false;
        }
        //TODO сделать проверку свободного места в слоте. Т.е. если в слоте есть свободное место,то сначала заполняем его и только потом следущий

        public bool TryGetItemFromBar(ItemType itemType, out Item item)
        {
            item = null;
            foreach (var slot in ItemsSlots)
            {
                if (!slot.IsEmpty && slot.ItemType == itemType)
                {
                    var result = slot.TryGetItem( out item); // Предполагается, что у BarItemsSlot есть метод GetItem
                    return result;
                }
            }

            return false;
        }

        private BarItemsSlot FindFreeSlot(ItemType itemType)
        {
            foreach (var slot in ItemsSlots)
            {
                if (slot.IsAvailable && slot.ItemType == itemType)
                {
                    return slot;
                }

                if (slot.IsEmpty)
                {
                    return slot;
                }
            }

            return null;
        }
    }
}