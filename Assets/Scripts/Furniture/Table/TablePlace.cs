using System.Collections;
using UnityEngine;
using Utilities;

namespace Furniture
{
    public class TablePlace : MonoBehaviour
    {
        private bool _isPurchasing;
        private float _elapsedTime;
        private int _remainingCost;
        private TableController _tableController;
        private Coroutine _purchaseCoroutine;
        
        private float _purchaseTime;
        private TableCostView _tableCostView;

        public void Initialize(TableController tableController, int costTable, float purchaseTime, TableCostView tableCostView)
        {
            _tableController = tableController;
            _purchaseTime = purchaseTime;
            _tableCostView = tableCostView;
            
            //TODO Перенести в Prefs;
            _remainingCost = costTable;
            _tableCostView.UpdateTableCost(_remainingCost);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(GlobalConstants.Player) && !_isPurchasing)
            {
                if (_purchaseCoroutine != null)
                {
                    StopCoroutine(_purchaseCoroutine);
                }
                
                _purchaseCoroutine = StartCoroutine(PurchaseTableCoroutine());
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GlobalConstants.Player) && _isPurchasing)
            {
                if (_purchaseCoroutine != null)
                {
                    StopCoroutine(_purchaseCoroutine);
                    _isPurchasing = false;
                }
            }
        }
        
        private IEnumerator PurchaseTableCoroutine()
        {
            _isPurchasing = true;
            _elapsedTime = 0f;

            var startingCost = _remainingCost;
            var leftOverPurchaseTime = _purchaseTime - _elapsedTime;
            

            while (_elapsedTime < _purchaseTime)
            {
                var purchasedProgress = _elapsedTime / leftOverPurchaseTime;
                
                _remainingCost = Mathf.RoundToInt(Mathf.Lerp(startingCost, 0, purchasedProgress));

                _tableCostView.UpdateTableCost(_remainingCost);
                
                yield return null;
                _elapsedTime += Time.deltaTime;
                
                leftOverPurchaseTime = _purchaseTime - _elapsedTime;
            }
            
            _isPurchasing = false;
            
            if (_remainingCost <= 0)
            {
                _tableController.SetTableBoughtState();
            }
        }
    }
}