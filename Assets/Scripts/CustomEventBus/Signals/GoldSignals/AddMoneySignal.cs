namespace CustomEventBus.Signals.GoldSignals
{
    public class AddMoneySignal : EventSignal
    {
        public readonly int Value;

        public AddMoneySignal(int value)
        {
            Value = value;
        }
    }
}