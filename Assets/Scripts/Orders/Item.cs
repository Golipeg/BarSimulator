using System;
using UnityEngine;

namespace Orders
{
    public class Item : MonoBehaviour

    {
        public ItemType ItemType => _itemModel.ItemType;
        private Vector3 _itemSize;
        private ItemModel _itemModel;


        public void Initialize(ItemModel itemModel)
        {
            _itemModel = itemModel;
            _itemSize = GetComponent<Renderer>().bounds.size;
        }

        public void GiveItemForHolders(Transform holderTransform)
        {
            transform.SetParent(holderTransform);
            // Если позиция не задана, рассчитываем новую

            Vector3 nextPosition = Vector3.zero;
            // Проверяем, есть ли уже дочерние элементы у holderTransform
            if (holderTransform.childCount > 0)
            {
                Transform lastChild = holderTransform.GetChild(holderTransform.childCount - 1);
                Vector3 lastItemPosition = lastChild.localPosition;
                nextPosition = lastItemPosition + new Vector3(0, this._itemSize.y, 0);
            }

            transform.localPosition = nextPosition;
        }
    }
}