using System;

namespace Oms.Application.Contracts.CollaborationServices.ThreePL
{
    /// <summary>
    /// 创建系统仓返回出参
    /// </summary>
    public class SystemWarehouseDto
    {
        public string Id { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseCode { get; set; }
        public string WarehouseName { get; set; }
        public int InterfaceWarehouseId { get; set; }
        public string InterfaceWarehouseCode { get; set; }
        public string InterfaceWarehouseName { get; set; }
    }
}
