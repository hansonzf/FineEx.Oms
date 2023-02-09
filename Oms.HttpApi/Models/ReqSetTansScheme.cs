namespace Oms.HttpApi.Models
{
    public class ReqSetTansScheme
    {
        /// <summary>
        /// 订单List
        /// </summary>
        public List<long> OrderId { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 运输方案匹配类型(1=现有运输方案 2=手动调度)
        /// </summary>
        public int MatchType { get; set; }
        /// <summary>
        /// 运输方案名称id
        /// </summary>
        public string AvaliableTranPlanId { get; set; }
        /// <summary>
        /// 受理类型(1=出库 2=入库 3=物流运输)
        /// </summary>
        public int ReceiptType { get; set; }
        /// <summary>
        /// 运输方案名称(现有运输方案)
        /// </summary>
        public string TranPlanName { get; set; }
        /// <summary>
        /// 运输方案描述(现有运输方案)
        /// </summary>
        public string TranPlanMemo { get; set; }

        /// <summary>
        /// 运输方案明细
        /// </summary>
        public List<TranPlanDetail> TranPlanDetails { get; set; }
    }

    public class TranPlanDetail
    {
        /// <summary>
        /// 资源类别(1=运力资源 2=中转地)
        /// </summary>
        public int ResourceType { get; set; }
        /// <summary>
        /// 资源名称(中转地)
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 资源id
        /// </summary>
        public string ResourceId { get; set; }
        /// <summary>
        /// 联系人(中转地)
        /// </summary>
        public string Contacter { get; set; }
        /// <summary>
        /// 省(中转地)
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市(中转地)
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区(中转地)
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 手机号(中转地)
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 地址(中转地)
        /// </summary>
        public string Address { get; set; }
    }
}
