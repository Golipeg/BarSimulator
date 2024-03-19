using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals.Table;
using Furniture;
using UnityEngine;
using UnityEngine.AI;

namespace Managers
{
    public class LoungeManager : MonoBehaviour
    {
        [SerializeField] private List<TableController> _tables;
        private bool isFirstTableBought;

        public void Initialize()
        {
            foreach (var tableController in _tables)
            {
                tableController.Initialize();
                tableController.HaveBoughtTable += SendFirstTableIsBoughtSignal;
            }
        }

        private void SendFirstTableIsBoughtSignal()
        {
            EventStreams.CustomEventBus.Publish(new FirstTableIsBoughtSignal());

            foreach (var tableController in _tables)
            {
                tableController.HaveBoughtTable -= SendFirstTableIsBoughtSignal;
            }
        }
    }
}
