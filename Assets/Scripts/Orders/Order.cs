using System.Collections.Generic;

namespace Orders
{
    public class Order
    {
        public IReadOnlyDictionary<ItemType, int> Orders => _orders;

        private Dictionary<ItemType, int> _orders = new();
        private int _totalCupQuantity;
        private float _totalCost;


        public void AddItemToOrder(ItemType itemType, int count, float totalCost)
        {
            if (!_orders.ContainsKey(itemType))
            {
                _orders.Add(itemType, count);
            }
            else
            {
                _orders[itemType]+=count;
            }

            _totalCupQuantity += count;
            _totalCost += totalCost;
        }

        public int GetTotalCupQuantity()
        {
            return _totalCupQuantity;
        }


        public float GetTotalCost()
        {
            return _totalCost;
        }
    }
}