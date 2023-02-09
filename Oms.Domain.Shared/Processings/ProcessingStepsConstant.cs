namespace Oms.Domain.Processings
{
    public class ProcessingStepsConstant
    {
        /// <summary>
        /// 系统中所支持的完全处理步骤列表
        /// 这个列表中的项和 ProcessingSteps 枚举的位相对应
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
        /// 仓配一体出库单的处理项
        /// </summary>
        public static ProcessingSteps OutboundWithTransportSteps => 
            ProcessingSteps.B2bCheckoutInventory | ProcessingSteps.MatchTransport | ProcessingSteps.Dispatching;

        /// <summary>
        /// 仓配一体入库单的处理项
        /// </summary>
        public static ProcessingSteps InboundWithTransportSteps =>
            ProcessingSteps.MatchTransport | ProcessingSteps.Dispatching;

        /// <summary>
        /// 运输订单的处理项
        /// </summary>
        public static ProcessingSteps TransportSteps =>
            ProcessingSteps.MatchTransport | ProcessingSteps.Dispatching;
    }
}
