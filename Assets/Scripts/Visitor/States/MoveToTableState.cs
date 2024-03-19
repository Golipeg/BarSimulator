using System;
using CustomEventBus;
using CustomEventBus.Signals.Table;
using CustomEventBus.Signals.Visitor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using FirstTableIsBoughtSignal = CustomEventBus.Signals.Table.FirstTableIsBoughtSignal;
using IState = IngameStateMachine.IState;
using StateMachine = IngameStateMachine.StateMachine;

namespace Visitor
{
    public class MoveToTableState : MonoBehaviour, IState
    {
        private StateMachine _stateMachine;
        private NavMeshAgent _visitorNavMeshAgent;
        private Vector3 _chairPosition;
        private Visitor _visitor;
        private BarQueueManager _barQueueManager;

        public void Construct(Visitor visitor, BarQueueManager barQueueManager)
        {
            _visitor = visitor;
            _visitorNavMeshAgent = _visitor.NavMeshAgent;
            _barQueueManager = barQueueManager;
        }


        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            EventStreams.CustomEventBus.Subscribe<PositionFreeTableSignal>(MoveToFreeTable);
        }

        private void MoveToFreeTable(PositionFreeTableSignal signal)
        {
            _chairPosition = signal.FreePlace.position;
            if (_chairPosition != null)
            {
                var visitor = _barQueueManager.DequeueVisitor();
                if (visitor != null)
                {
                    visitor.NavMeshAgent.SetDestination(_chairPosition);
                }
            }
        }


        private void Update()
        {
            if (_visitorNavMeshAgent.remainingDistance <= _visitorNavMeshAgent.stoppingDistance &&
                !_visitorNavMeshAgent.pathPending)
            {
                if (_visitorNavMeshAgent.velocity.sqrMagnitude <= 0f && !_visitorNavMeshAgent.hasPath)
                {
                    _stateMachine.Enter<DrinkBevaragesState>();
                }
            }
        }

        public void OnExit()
        {
            EventStreams.CustomEventBus.Unsubscribe<PositionFreeTableSignal>(MoveToFreeTable);
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
    
}