using DefaultNamespace;
using Orders;
using Pools;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using StateMachine = IngameStateMachine.StateMachine;

namespace Visitor
{
    public class VisitorFactory : MonoBehaviour
    {
        public Visitor VisitorPrefab => _visitorPrefab;
        [SerializeField] private Transform _barPosition;
        [SerializeField] private Visitor _visitorPrefab;
        [SerializeField] private BarQueueManager _barQueueManager;
        [SerializeField] private OrdersFactory _ordersFactory;


        private Transform _enterExitTransform;

        public void Initialize(Transform enterExitTransform)
        {
            _enterExitTransform = enterExitTransform;
        }


        public Visitor Create()
        {
            var visitor = Instantiate(_visitorPrefab, _enterExitTransform.position, Quaternion.identity);
            var order = _ordersFactory.CreateRandomOrder();

            var moveToBarState = visitor.AddComponent<MoveToBarState>();
            moveToBarState.Construct(_barPosition, visitor, _barQueueManager);

            var handleOrderState = visitor.AddComponent<HandleOrderState>();
            handleOrderState.Construct(order);


            var moveToTableState = visitor.AddComponent<MoveToTableState>();
            moveToTableState.Construct(visitor, _barQueueManager);

            var drinkBeverageState = visitor.AddComponent<DrinkBevaragesState>();
            drinkBeverageState.Construct(order);

            var moveToExitState = visitor.AddComponent<MoveToExitState>();
            moveToExitState.Construct(_enterExitTransform.position, visitor);
            visitor.BehaviourStateMachine = new StateMachine(moveToBarState, handleOrderState,
                moveToTableState, drinkBeverageState, moveToExitState);
            visitor.BehaviourStateMachine.Initialize();

            return visitor;
        }
    }
}