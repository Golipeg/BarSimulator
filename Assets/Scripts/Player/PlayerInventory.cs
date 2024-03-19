using System;
using System.Collections.Generic;
using DefaultNamespace;
using Furniture.ProductionMachine;
using Orders;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour

    {
        [field: SerializeField] public int MaxItemCount { get; private set; }
        [SerializeField] private Transform _placeHolder;
        private Stack<Item> _items = new Stack<Item>();

        public void AddItem(Item item)
        {
            _items.Push(item);
        }

        private void OnTriggerStay(Collider collider)
        {
            Debug.Log(collider.name);
            if (collider.TryGetComponent(out BarManager barManager))
            {
                if (_items.Count == 0)
                {
                    return;
                }

                var item = _items.Peek();
                var result = barManager.TryAddItemToBar(item);
                if (result)
                {
                    _items.Pop();
                }
            }

            if (collider.TryGetComponent(out ProductMachine productionMachine))
            {
                if (_items.Count < MaxItemCount && productionMachine.TryGetItem(out Item item))
                {
                    AddItem(item);
                    item.GiveItemForHolders(_placeHolder);
                }
            }
        }
        //TODO дописать логику добавления item Player 
    }
}