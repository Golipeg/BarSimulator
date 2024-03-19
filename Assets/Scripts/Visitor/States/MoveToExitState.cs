using System;
using CustomEventBus;
using CustomEventBus.Signals.Visitor;
using IngameStateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace Visitor
{
    public class MoveToExitState : MonoBehaviour, IState

    {
        private Vector3 _exitPosition;
        private StateMachine _stateMachine;
        private NavMeshAgent _visitorNavMeshAgent;
        private Visitor _visitor;
        private bool _isEnableUpdate;

        public void Construct(Vector3 enterExitPosition, Visitor visitor)
        {
            _exitPosition = enterExitPosition;
            _visitorNavMeshAgent = visitor.NavMeshAgent;
            _visitor = visitor;
        }

        public void SetStateMachine(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            _visitorNavMeshAgent.SetDestination(_exitPosition);
            _isEnableUpdate = true;
        }

        private void Update()
        {
            if (!_isEnableUpdate)
            {
                return;
            }

            if (_visitorNavMeshAgent.remainingDistance <= _visitorNavMeshAgent.stoppingDistance &&
                !_visitorNavMeshAgent.pathPending)
            {
                if (_visitorNavMeshAgent.velocity.sqrMagnitude <= 0f && !_visitorNavMeshAgent.hasPath)
                {
                    //EventStreams.CustomEventBus.Publish(new VisitorLeftBarSignal());
                    _visitor.VisitorLeft?.Invoke(_visitor);
                }
            }
        }

        public void OnExit()
        {
            _isEnableUpdate = false;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}