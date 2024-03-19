using UnityEngine;

namespace Furniture
{
    public class Chair : MonoBehaviour
    {
        [SerializeField] private Transform _placeStartSeating;
        
        private bool _isChairBusy = false;

        public void SetBusyStatus(bool status)
        {
            _isChairBusy = status;
        }

        public bool GetChairBusyStatus()
        {
            return _isChairBusy;
        }

        public Transform ReturnFreeChairPlace()
        {
            return _placeStartSeating;
        }
    }
}