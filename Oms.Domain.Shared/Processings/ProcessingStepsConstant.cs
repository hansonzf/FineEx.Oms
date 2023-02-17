namespace Oms.Domain.Processings
{
    public class ProcessingStepsConstant
    {
        /// <summary>
        /// There defined all the steps which supported in system
        /// </summary>
        public static List<ProcessingSteps> FullyProcessingSteps =>
            new List<ProcessingSteps>
            {
                ProcessingSteps.B2bCheckinInventory,
                ProcessingSteps.B2bCheckoutInventory,
                ProcessingSteps.MatchTransport,
                ProcessingSteps.Dispatching
            };

        /// <summary>
        /// outbound warehouse and transport business contains these processing steps
        /// </summary>
        public static ProcessingSteps OutboundWithTransportSteps => 
            ProcessingSteps.B2bCheckoutInventory | ProcessingSteps.MatchTransport | ProcessingSteps.Dispatching;

        /// <summary>
        /// inbound warehouse and transport business contains these processing steps
        /// </summary>
        public static ProcessingSteps InboundWithTransportSteps =>
            ProcessingSteps.MatchTransport | ProcessingSteps.Dispatching;

        /// <summary>
        /// transport business contains these processing steps
        /// </summary>
        public static ProcessingSteps TransportSteps =>
            ProcessingSteps.MatchTransport | ProcessingSteps.Dispatching;
    }
}
