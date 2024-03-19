using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Orders
{
    [Serializable]
    public class ItemModel
    {
        [field: SerializeField] public ItemType ItemType { get; private set; }
        [field: SerializeField] public Item ItemPrefab { get; private set; }
        [field: SerializeField] public float Price { get; private set; }

        [FormerlySerializedAs("IsEnableItem")] [field: SerializeField]
        public bool IsAvailableItem;
    }
}