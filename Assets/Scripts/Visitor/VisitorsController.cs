using System.Collections;
using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using CustomEventBus.Signals.Table;
using Pools;
using UnityEngine;

namespace Visitor
{
    public class VisitorsController : MonoBehaviour
    {
        private const int MAX_VISITORS_COUNT = 5;
        [SerializeField] private VisitorFactory _visitorFactory;
        [SerializeField] private Transform _enterExitTransform;
        [SerializeField] private float _spawnDelay;
        private MonoBehaviourPool<Visitor> _visitorPool;
        private WaitForSeconds _waitForSeconds;

        public void Initialize()
        {
            _visitorFactory.Initialize(_enterExitTransform);
            InitPool();
            EventStreams.CustomEventBus.Subscribe<FirstTableIsBoughtSignal>(StartSpawnVisitors);
            _waitForSeconds = new WaitForSeconds(_spawnDelay);
        }

        private void InitPool()
        {
            _visitorPool = new MonoBehaviourPool<Visitor>(_visitorFactory.VisitorPrefab, transform, MAX_VISITORS_COUNT);
            for (int i = 0; i < MAX_VISITORS_COUNT; i++)
            {
                var visitor = _visitorFactory.Create();
                _visitorPool.Release(visitor);
            }
        }

        private void StartSpawnVisitors(FirstTableIsBoughtSignal obj)
        {
            StartCoroutine(SpawnVisitorsCoroutine());
        }

        private IEnumerator SpawnVisitorsCoroutine()
        {
            while (true)
            {
                while (_visitorPool.UsedItems.Count < MAX_VISITORS_COUNT)
                {
                    SpawnVisitor();
                    yield return _waitForSeconds;
                }

                yield return null;
            }
        }

        private void SpawnVisitor()
        {
            var visitor = _visitorPool.Take();
            visitor.BehaviourStateMachine.Enter<MoveToBarState>();
            visitor.VisitorLeft += OnVisitorLeft;
        }

        private void OnVisitorLeft(Visitor visitor)
        {
            _visitorPool.Release(visitor);
        }
    }
}