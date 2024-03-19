using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Orders
{
    [CreateAssetMenu(fileName = "ItemDataBase", menuName = "ItemDataBase")]
    public class ItemDataBase : ScriptableObject

    {
        [field: SerializeField] public List<ItemModel> Items;

        public ItemModel GetItemModel(ItemType itemType)
        {
            return Items.First(i => itemType == i.ItemType);
            
        }

        private void OnValidate()
        {
            HashSet<ItemType> itemTypes = new HashSet<ItemType>();
            foreach (ItemModel item in Items)
            {
                if (!itemTypes.Add(item.ItemType))
                {
                    Debug.LogWarning("Найден дубликат ItemType: " + item.ItemType);
                }
            }
        }
    }
}