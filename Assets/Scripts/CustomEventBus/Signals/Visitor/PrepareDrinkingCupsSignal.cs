namespace CustomEventBus.Signals.Visitor
{
    public class PrepareDrinkingCupsSignal : EventSignal
    {
        public readonly int Cups;
        
        public PrepareDrinkingCupsSignal(int cups)
        {
            Cups = cups;
        }
    }
}