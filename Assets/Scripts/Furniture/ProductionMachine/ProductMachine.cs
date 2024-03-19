using System.Collections;
using System.Collections.Generic;
using Orders;
using UnityEngine;

namespace Furniture.ProductionMachine
{
    public class ProductMachine : MonoBehaviour
    {
        [SerializeField] private int _maxCountItems = 5;
        private Stack<Item> _producedItems = new Stack<Item>();
        [SerializeField] private ItemType _itemType;
        [SerializeField] private ItemDataBase _itemDataBase;
        [SerializeField] private float _produceOneItemTime = 2f;

        private void Start()
        {
            StartCoroutine(ProduceItemsCoroutine());
        }

        private IEnumerator ProduceItemsCoroutine()
        {
            while (true)
            {
                if (_producedItems.Count < _maxCountItems)
                {
                    yield return new WaitForSeconds(_produceOneItemTime);
                    var producedItem = ProduceItem();
                    _producedItems.Push(producedItem);
                }
                else
                {
                    yield return null;
                }
            }
        }

        public bool TryGetItem(out Item item)
        {
            item = null;
            if (_producedItems.Count != 0)
            {
                item = _producedItems.Pop();
                return true;
            }

            return false;
        }

        private Item ProduceItem()
        {
            var itemModel = _itemDataBase.GetItemModel(_itemType);
            var item = Instantiate(itemModel.ItemPrefab,transform,false);
            item.Initialize(itemModel);

            item.GiveItemForHolders(transform);

            return item;
        }
    }
}