using System;
using CustomEventBus;
using CustomEventBus.Signals.Player;
using CustomEventBus.Signals.ProductionMachine;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private int _maxItems = 8;

        private PlayerStateMachine _stateMachine;
        private int _currentItem = 0;

        private InputController _input;
        public InputController InputController => _input;

        private void Awake()
        {
            _input = GetComponent<InputController>();
        }

        private void Start()
        {
            _stateMachine = new PlayerStateMachine();
            _stateMachine.ChangeState(new MoveState(_speed), this); // Начальное состояние - перемещение

            //TODO ARTEM
            EventStreams.CustomEventBus.Subscribe<ItemCanBeTakePlayerSignal>(TakeItem);
        }

        //TODO ARTEM
        private void TakeItem(ItemCanBeTakePlayerSignal signal)
        {
            if (_currentItem < _maxItems)
            {
                _currentItem++;
                EventStreams.CustomEventBus.Publish(new PlayerTookItemSignal());
            }

            //TODO ARTEM
            //TODO Delete for check
            if (_currentItem == _maxItems)
            {
                Debug.Log("Full");
            }
        }

        private void Update()
        {
            _stateMachine.Update(this);
        }

        private void OnDestroy()
        {
            EventStreams.CustomEventBus.Unsubscribe<ItemCanBeTakePlayerSignal>(TakeItem);
        }
    }
}