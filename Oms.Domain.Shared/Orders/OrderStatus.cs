﻿using System.ComponentModel;

namespace Oms.Domain.Orders
{
    public enum OrderStatus
    {
        [Description("待处理")]
        Created,
        Submited,
        CheckingStock,
        StockChecked,
        MatchingTransportLine,
        TransportLineMatched,
        [Description("待下发")]
        WaitforDispatch,
        Dispatched,
        [Description("待拣货")]
        ReadyToSorting,
        [Description("拣货中")]
        Sorting,
        [Description("完成拣货待出库")]
        ReadyToStockout,
        [Description("已出库待装车")]
        WaitforLoading,
        [Description("运输中")]
        Transferring,
        [Description("待派送")]
        ReadyToDelivery,
        [Description("派送中")]
        Delivering,
        [Description("已签收")]
        Signed,
        [Description("已完成")]
        Completed,
        [Description("已取消")]
        Cancelled
    }
}