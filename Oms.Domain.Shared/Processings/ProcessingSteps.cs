namespace Oms.Domain.Processings
{
    [Flags]
    public enum ProcessingSteps
    {
        None = 0,
        B2bCheckinInventory = 1,
        B2bCheckoutInventory = 2,
        MatchTransport = 4,
        Dispatching = 524288
    }
}
