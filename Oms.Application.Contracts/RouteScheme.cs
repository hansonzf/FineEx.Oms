using Oms.Domain.Orders;

namespace Oms.Application.Contracts
{
    public class RouteScheme
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Memo { get; set; }

        public List<RouteSchemeItemData> Items { get; set; }

        public List<RouteSchemeLocation> Locations { get; set; }
    }

    public class RouteSchemeItemData
    {

        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 线路方案id
        /// </summary>
        public Guid RouteSchemeId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 运力/中转
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 运力/中转id
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 运力/中转名称
        /// </summary>
        public string Name { get; set; }

    }

    public class RouteSchemeLocation
    {

        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 线路方案id
        /// </summary>
        public Guid RouteSchemeId { get; set; }

        /// <summary>
        /// 始发地/收货地
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 按地址/按省市区
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 地址id
        /// </summary>
        public string LocationId { get; set; }

        /// <summary>
        /// 地址名称
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

    }
}
