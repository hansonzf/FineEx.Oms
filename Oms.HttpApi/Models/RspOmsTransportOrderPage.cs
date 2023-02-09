namespace Oms.HttpApi.Models
{
    public class RspOmsTransportOrderPage
    {
        public bool Flag { get; set; }

        public string Code { get; set; }

        public string Message { get; set; }
        /// <summary>
        /// 盘点单查询 返回数据
        /// </summary>
        public List<RspOmsTransportOrder> Data { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }       
    }

    public class RspOmsTransportOrder
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; } = 0;
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string SyncOrderCode { get; set; } = "";
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 运单状态
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 运单状态名称
        /// </summary>
        public string OrderStatusName { get; set; }
        /// <summary>
        /// 运输方案Id
        /// </summary>
        public long TransPlanId { get; set; }
        /// <summary>
        /// 运输方案名称
        /// </summary>
        public string TransPlanName { get; set; }
        /// <summary>
        /// 客户Id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 寄件单位
        /// </summary>
        public string FromAddressName { get; set; }
        /// <summary>
        /// 寄件地址
        /// </summary>
        public string FromAddress { get; set; }
        /// <summary>
        /// 寄件联系人姓名
        /// </summary>
        public string FromContacter { get; set; }
        /// <summary>
        /// 寄件联系人手机号
        /// </summary>
        public string FromPhone { get; set; }
        /// <summary>
        /// 寄件地址Id
        /// </summary>
        public string FromAddressId { get; set; }
        /// <summary>
        /// 收件单位
        /// </summary>
        public string ToAddressName { get; set; }
        /// <summary>
        /// 收件地址
        /// </summary>
        public string ToAddress { get; set; }
        /// <summary>
        /// 收件联系人姓名
        /// </summary>
        public string ToContacter { get; set; }
        /// <summary>
        /// 收件联系人手机号
        /// </summary>
        public string ToPhone { get; set; }
        /// <summary>
        /// 收件地址Id
        /// </summary>
        public string ToAddressId { get; set; }
        /// <summary>
        /// 包裹总数量
        /// </summary>
        public int TotalAmount { get; set; }
        /// <summary>
        /// 包裹总重量
        /// </summary>
        public decimal TotalWeight { get; set; }
        /// <summary>
        /// 包裹总体积
        /// </summary>
        public decimal TotalVolume { get; set; }
        /// <summary>
        /// 运输费用（元）
        /// </summary>
        public double TransportFee { get; set; }
        /// <summary>
        /// 期望提货时间
        /// </summary>
        public DateTime PlanPickDate { get; set; }

        /// <summary>
        /// 实际提货时间
        /// </summary>
        public DateTime? RealPickDate { get; set; }

        /// <summary>
        /// 期望送达时间
        /// </summary>
        public DateTime PlanDeliveryDate { get; set; }

        /// <summary>
        /// 实际送达时间
        /// </summary>
        public DateTime? RealDeliveryDate { get; set; }
        /// <summary>
        /// 签收类型
        /// </summary>
        public int SignType { get; set; }
        /// <summary>
        /// 提货图片数量
        /// </summary>
        public int PickImageNum { get; set; }
        /// <summary>
        /// 是否回单
        /// </summary>
        public int IsBack { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public int OrderType { get; set; }
        /// <summary>
        /// 订单类型名称
        /// </summary>
        public string OrderTypeName { get; set; }
        /// <summary>
        /// 订单来源类型
        /// </summary>
        public int SourceType { get; set; }
        /// <summary>
        /// 订单来源类型名称
        /// </summary>
        public string SourceTypeName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedBy { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatedName { get; set; }
    }
}
