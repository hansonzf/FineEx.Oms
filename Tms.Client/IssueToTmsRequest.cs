namespace Oms.Application.Contracts.CollaborationServices.Tms
{
    public class IssueToTmsRequest
    {
        /// <summary>
        /// 上层订单Id
        /// </summary>
        public string UpOrderCode { get; set; }
        /// <summary>
        /// 入库单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 运单号
        /// </summary>
        public string OrderCode { get; set; }

        /// <summary>
        /// 运单类型
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 业务类型 1发运 2退货 3调拨
        /// </summary>
        public int OperateType { get; set; }

        /// <summary>
        /// 客户Id
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 承运商Id
        /// </summary>
        public string CarrierId { get; set; }

        /// <summary>
        /// 承运商名称
        /// </summary>
        public string CarrierName { get; set; }

        /// <summary>
        /// 包裹总数量
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// 包裹总体积
        /// </summary>
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// 包裹总重量
        /// </summary>
        public decimal TotalWeight { get; set; }

        /// <summary>
        /// 包裹总内件数
        /// </summary>
        public int TotalDoubleAmount { get; set; }

        /// <summary>
        /// 运输方式
        /// </summary>
        public int TransportType { get; set; }

        /// <summary>
        /// 预计提货时间
        /// </summary>
        public DateTime PlanPickDate { get; set; }

        /// <summary>
        /// 期望送达时间
        /// </summary>
        public DateTime PlanDeliveryDate { get; set; }

        /// <summary>
        /// 起始省
        /// </summary>
        public string FromProvince { get; set; }
        /// <summary>
        /// 起始市
        /// </summary>
        public string FromCity { get; set; }
        /// <summary>
        /// 起始区
        /// </summary>
        public string FromArea { get; set; }
        /// <summary>
        /// 起始详细地址
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// 起始地址名称
        /// </summary>
        public string FromAddressName { get; set; }
        /// <summary>
        /// 起始联系人姓名
        /// </summary>
        public string FromContacter { get; set; }
        /// <summary>
        /// 起始联系人手机号
        /// </summary>
        public string FromPhone { get; set; }
        /// <summary>
        /// 起始经度
        /// </summary>
        public decimal FromLongitude { get; set; }
        /// <summary>
        /// 起始纬度
        /// </summary>
        public decimal FromLatitude { get; set; }
        /// <summary>
        /// 起始地址Id
        /// </summary>
        public string FromAddressId { get; set; }
        /// <summary>
        /// 目的省
        /// </summary>
        public string ToProvince { get; set; }
        /// <summary>
        /// 目的市
        /// </summary>
        public string ToCity { get; set; }
        /// <summary>
        /// 目的区
        /// </summary>
        public string ToArea { get; set; }
        /// <summary>
        /// 目的详细地址
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// 目的地址名称
        /// </summary>
        public string ToAddressName { get; set; }
        /// <summary>
        /// 目的联系人姓名
        /// </summary>
        public string ToContacter { get; set; }
        /// <summary>
        /// 目的联系人手机号
        /// </summary>
        public string ToPhone { get; set; }
        /// <summary>
        /// 目的经度
        /// </summary>
        public decimal ToLongitude { get; set; }
        /// <summary>
        /// 目的纬度
        /// </summary>
        public decimal ToLatitude { get; set; }
        /// <summary>
        /// 目的地址Id
        /// </summary>
        public string ToAddressId { get; set; }

        /// <summary>
        /// 货物信息列表
        /// </summary>
        public List<ToTmsOrderGoods> OrderGoods { get; set; }
    }

    public class ToTmsOrderGoods
    {
        /// <summary>
        /// 入库单Id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 货物码
        /// </summary>
        public string GoodsCode { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        public string LoadName { get; set; }
        /// <summary>
        /// 货物数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 货物体积
        /// </summary>
        public decimal Volume { get; set; } = 0;
        /// <summary>
        /// 货物重量
        /// </summary>
        public decimal Weight { get; set; } = 0;
        /// <summary>
        /// 货物内件数
        /// </summary>
        public int DoubleAmount { get; set; } = 0;
    }
}
