using TMPro;
using UnityEngine;

namespace Furniture
{
    public class TableCostView : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _tableRemainCost;
        
        public void UpdateTableCost(int remainCost)
        {
            _tableRemainCost.text = remainCost.ToString();
        }
    }
}