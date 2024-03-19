using System.Collections;
using System.Linq;
using CustomEventBus;
using CustomEventBus.Signals.Table;
using CustomEventBus.Signals.Visitor;
using UnityEngine;
using Utilities;

namespace Furniture
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private Chair[] _chairs;
        [SerializeField] private float _timeSeatingForCup = 1f;
        [SerializeField] private GameObject _dirtyDishesVisual;

        private float _itemCount = 0f;
        private float _dirtyItemsCount = 0f;
        private bool _isTableBusy = false;
        private Coroutine _takeDirtyItemsCoroutine;

        //На будущие - Анимация летящей кружки на игрока
        private Animator _animator;
        private static readonly int TakeCup = Animator.StringToHash("TakeCup");

        public void Initialize()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            EventStreams.CustomEventBus.Subscribe<FindFreeTableSignal>(ReturnFreeChair);
            EventStreams.CustomEventBus.Subscribe<PrepareDrinkingCupsSignal>(SetDrinkingTime);
        }
        
        private void SetDrinkingTime(PrepareDrinkingCupsSignal signal)
        {
            _itemCount += signal.Cups;

            CheckTableFreePlaces();
        
            if (_isTableBusy)
            {
                StartCoroutine(StartDrinking());
            }
        }
    
        private IEnumerator StartDrinking()
        {
            while (_itemCount != 0)
            {
                _itemCount -= 1;
            
                _dirtyItemsCount++;

                yield return new WaitForSeconds(_timeSeatingForCup);
            }

            if (_itemCount == 0)
            {
                LeaveTable();
            }
        }

        private void LeaveTable()
        {
            StopCoroutine(StartDrinking());
            EventStreams.CustomEventBus.Publish(new VisitorEndedDrinkingSignal());
        
            _dirtyDishesVisual.SetActive(true);
        }

        private void ReturnFreeChair(FindFreeTableSignal signal)
        {
            if (_isTableBusy)
            {
                return;
            }
        
            foreach (var chair in _chairs)
            {
                if (!chair.GetChairBusyStatus())
                {
                    chair.SetBusyStatus(true);
                    EventStreams.CustomEventBus.Publish(new PositionFreeTableSignal(chair.ReturnFreeChairPlace()));
                    break;
                }
            }

            CheckTableFreePlaces();
        }

        private void CheckTableFreePlaces()
        {
            if (_chairs.Any(chair => !chair.GetChairBusyStatus()))
            {
                _isTableBusy = false;
                return;
            }

            _isTableBusy = true;
        }
    
        //TODO Нужно сделать проверку, что у плера еще есть место в руках и он может взять больше или в корутине узнавать
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(GlobalConstants.Player) && _dirtyItemsCount > 0)
            {
                if (_takeDirtyItemsCoroutine != null)
                {
                    StopCoroutine(_takeDirtyItemsCoroutine);
                }
                
                _takeDirtyItemsCoroutine = StartCoroutine(TakeDirtyCupsCoroutine());
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GlobalConstants.Player))
            {
                if (_takeDirtyItemsCoroutine != null)
                {
                    StopCoroutine(_takeDirtyItemsCoroutine);
                }
            }
        }
    
        private IEnumerator TakeDirtyCupsCoroutine()
        {
            while (_dirtyItemsCount > 0)
            {
                _dirtyItemsCount--;
                _animator.SetTrigger(TakeCup);
            
                yield return null;
            }

            if (_dirtyItemsCount == 0)
            {
                _dirtyDishesVisual.SetActive(false);
                _isTableBusy = false;
                StopCoroutine(_takeDirtyItemsCoroutine);
            }
        }

        public void OnDestroy()
        {
            EventStreams.CustomEventBus.Unsubscribe<FindFreeTableSignal>(ReturnFreeChair);
            EventStreams.CustomEventBus.Unsubscribe<PrepareDrinkingCupsSignal>(SetDrinkingTime);
        }
    }
}