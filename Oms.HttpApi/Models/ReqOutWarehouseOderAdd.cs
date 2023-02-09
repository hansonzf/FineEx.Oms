using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oms.HttpApi.Models
{
    public class ReqOutWarehouseOderAdd
    {
        /// <summary>
        /// 出库单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 货主id
        /// </summary>
        public int CargoowerId { get; set; }
        /// <summary>
        /// 货主名称
        /// </summary>
        public string CargoowerName { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户单号
        /// </summary>
        public string CustomerOrderCode { get; set; }
        /// <summary>
        /// 出库仓库id
        /// </summary>
        public int WarehouseId { get; set; }
        /// <summary>
        /// 出库接口仓库id
        /// </summary>
        public int InterfaceWarehouseId { get; set; }
        /// <summary>
        /// 出库仓库
        /// </summary>
        public string? WarehouseIdName { get; set; }
        /// <summary>
        /// 出库类型
        /// </summary>
        public int OutWarehouseType { get; set; }
        /// <summary>
        /// 期望出库时间
        /// </summary>
        public string PlanOutWarehouseDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<Goods> CommodityInfoList { get; set; }
        /// <summary>
        /// 配送类型
        /// </summary>
        public int DeliveryType { get; set; }
        /// <summary>
        /// 配送信息
        /// 收货地址id，包含接口收货人（单位、姓名、手机号、详细地址、省市区）
        /// </summary>
        public string ConsigneeId { get; set; }
        /// <summary>
        /// 配送信息
        /// </summary>
        public string ConsigneeName { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// 收件手机
        /// </summary>
        public string ConsigneePhone { get; set; }
        /// <summary>
        /// 省市区
        /// </summary>
        public List<string> ConsigneeProvinceAreaCity { get; set; }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string ConsigneeAddress { get; set; }
        /// <summary>
        /// 期望送达时间
        /// </summary>
        public string ExpectDeliveryDate { get; set; }
    }
}
