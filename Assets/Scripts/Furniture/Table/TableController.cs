using System;
using UnityEngine;

namespace Furniture
{
    public class TableController : MonoBehaviour
    {
        public event Action HaveBoughtTable;

        [SerializeField] private TablePlace _tablePlace;
        [SerializeField] private Table _table;
        
        [SerializeField] private int _costTable = 60;
        [SerializeField] private float _purchaseTime = 4f;
        [SerializeField] private TableCostView _tableCostView;

        //ToDO добавить в префсы
        private bool _isTableBought;

        public void Initialize()
        {
            _tablePlace.Initialize(this,_costTable,_purchaseTime,_tableCostView);
            _table.Initialize();

            if (!_isTableBought)
            {
                _tablePlace.gameObject.SetActive(true);
                _table.gameObject.SetActive(false);
                return;
            }
            
            _tablePlace.gameObject.SetActive(true);
        }

        public void SetTableBoughtState()
        {
            _isTableBought = true;
            _tablePlace.gameObject.SetActive(false);
            _table.gameObject.SetActive(true);
            HaveBoughtTable?.Invoke();
        }
    }
}