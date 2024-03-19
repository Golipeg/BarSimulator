using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Orders
{
    public class OrdersFactory : MonoBehaviour
    {
        private const int MAX_ORDER_VALUE = 5;
        private const float MAX_ITEM_POSITIONS = 3;

        [SerializeField] private ItemDataBase _itemDataBase;

        public Order CreateRandomOrder()
        {
            var order = new Order();
            var selectedTypes = new List<ItemType>();

            var randomItemCount = Random.Range(1,
                Mathf.Min(MAX_ITEM_POSITIONS, _itemDataBase.Items.Count, selectedTypes.Count + 1));

            for (int i = 0; i < randomItemCount; i++)
            {
                var randomItemModel = GetRandomModel(selectedTypes);
                selectedTypes.Add(randomItemModel.ItemType);
                var randomItemTypeCount = Random.Range(1, MAX_ORDER_VALUE + 1);
                var totalCost = GetTotalCost(randomItemModel,randomItemTypeCount);
                order.AddItemToOrder(randomItemModel.ItemType, randomItemTypeCount,totalCost);
            }

            return order;
        }

        private float GetTotalCost(ItemModel randomItemModel, int randomItemTypeCount)
        {
            return randomItemModel.Price * randomItemTypeCount;
        }

        private ItemModel GetRandomModel(List<ItemType> selectedTypes)
        {
            var availableModels = _itemDataBase.Items
                .Where(item => item.IsAvailableItem && !selectedTypes.Contains(item.ItemType))
                .ToList();

            if (availableModels.Count > 0)
            {
                int randomIndex = Random.Range(0, availableModels.Count);
                return availableModels[randomIndex];
            }

            return null;
        }
    }
}