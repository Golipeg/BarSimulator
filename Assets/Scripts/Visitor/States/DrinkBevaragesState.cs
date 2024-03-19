using CustomEventBus;
using CustomEventBus.Signals.Table;
using CustomEventBus.Signals.Visitor;
using IngameStateMachine;
using Orders;
using UnityEngine;

namespace Visitor
{
    public class DrinkBevaragesState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;
        private Order _order;

        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            var cupQuantity = _order.GetTotalCupQuantity();
            EventStreams.CustomEventBus.Subscribe<VisitorEndedDrinkingSignal>(OnChangeState);
            EventStreams.CustomEventBus.Publish(new PrepareDrinkingCupsSignal(cupQuantity));
        }

        public void Construct(Order order)
        {
            _order = order;
        }

        private void OnChangeState(VisitorEndedDrinkingSignal signal)
        {
            _stateMachine.Enter<MoveToExitState>();
            
        }

        public void OnExit()
        {
            EventStreams.CustomEventBus.Unsubscribe<VisitorEndedDrinkingSignal>(OnChangeState);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}