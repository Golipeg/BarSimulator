using UnityEngine;

namespace CustomEventBus.Signals.Table
{
    public class PositionFreeTableSignal : EventSignal
    {
        public readonly Transform FreePlace;
        
        public PositionFreeTableSignal(Transform freePlace)
        {
            FreePlace = freePlace;
        }
    }
}