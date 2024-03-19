using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using StateMachine = IngameStateMachine.StateMachine;

namespace Visitor
{
    public class Visitor : MonoBehaviour
    {
        public StateMachine BehaviourStateMachine { get; set; }
        public NavMeshAgent NavMeshAgent { get; private set; }
        public Action<Visitor> VisitorLeft;


        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }


        public void SetDestination(Vector3 nextPosition)
        {
            NavMeshAgent.SetDestination(nextPosition);
        }
        
    }
}