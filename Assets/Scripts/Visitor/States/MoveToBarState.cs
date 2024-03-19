using System;
using IngameStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Visitor
{
    public class MoveToBarState : MonoBehaviour, IState
    {
        private Transform _barTransformPosition;
        private NavMeshAgent _visitorNavMeshAgent;
        private StateMachine _stateMachine;
        private BarQueueManager _barQueueManager;
        private Visitor _visitor;
        private bool _hasEnteredHandleOrderState;

        public void Construct(Transform barTransformPosition, Visitor visitor, BarQueueManager barQueueManager)
        {
            _barTransformPosition = barTransformPosition;
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
            _barQueueManager.EnqueueVisitor(_visitor, _barTransformPosition);
            _hasEnteredHandleOrderState = false;
        }

        private void Update()
        {
            if (_visitorNavMeshAgent.remainingDistance <= _visitorNavMeshAgent.stoppingDistance &&
                !_visitorNavMeshAgent.pathPending)
            {
                if (_visitorNavMeshAgent.velocity.sqrMagnitude <= 0f && !_visitorNavMeshAgent.hasPath)
                {
                    if (_barQueueManager.VisitorQueue.Count > 0 && _barQueueManager.VisitorQueue.Peek() == _visitor)
                    {
                        if (!_hasEnteredHandleOrderState)
                        {
                            
                            _stateMachine.Enter<HandleOrderState>();
                            _hasEnteredHandleOrderState = true; // Устанавливаем флаг после первого входа
                        }
                    }
                }
            }
        }

        public void OnExit()
        {
        }

        public void Dispose()
        {
        }
    }
}