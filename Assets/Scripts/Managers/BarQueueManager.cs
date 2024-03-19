using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Visitor
{
    public class BarQueueManager : MonoBehaviour

    {
        [SerializeField] private float _spacing = 1.0f;
        private Transform _queueStartPoint;
        public Queue<Visitor> VisitorQueue => _visitorsQueue;
        private Queue<Visitor> _visitorsQueue = new();

        public void EnqueueVisitor(Visitor visitor, Transform barPosition)
        {
            _queueStartPoint = barPosition;
            _visitorsQueue.Enqueue(visitor);


            UpdateQueuePositions();
        }

        [CanBeNull]
        public Visitor DequeueVisitor()
        {
            if (_visitorsQueue.Count > 0)
            {
                var visitor = _visitorsQueue.Dequeue();


                UpdateQueuePositions();
                return visitor;
            }

            return null;
        }

        private void UpdateQueuePositions()
        {
            int positionIndex = 0;
            foreach (Visitor visitor in _visitorsQueue)
            {
                float xPosition = _queueStartPoint.position.x + positionIndex * _spacing;
                var nextPosition = new Vector3(xPosition, _queueStartPoint.position.y, _queueStartPoint.position.z);
                visitor.SetDestination(nextPosition);
                positionIndex++;
            }
        }
    }
}