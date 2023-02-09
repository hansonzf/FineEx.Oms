using Newtonsoft.Json;
using Oms.Application.Contracts.CollaborationServices.Inventory;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Volo.Abp.DependencyInjection;

namespace Oms.Application.Jobs
{
    public class CheckInventoryInboundJobDataMapper : IJobDataMapper, ITransientDependency
    {
        public Dictionary<string, string> GrabJobData(IEnumerable<BusinessOrderDto> orders)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!orders.Any())
                return result;

            if (orders.First().BusinessType == BusinessTypes.InboundWithTransport)
            {
                result = GrabInboundOrder(orders.OfType<InboundOrderDto>());
            }

            return result;
        }

        private Dictionary<string, string> GrabInboundOrder(IEnumerable<InboundOrderDto> inboundOrders)
        {
            var result = new Dictionary<string, string>();

            //var request = new InventoryAuditWarehouseInRequest
            //{
            //    InType = 1,
            //    BusinessType = 0,
            //    BusinessID = inboundOrder.InboundId,
            //    MemberID = inboundOrder.CargoOwner.CustomerId,
            //    WarehouseID = inboundOrder.WarehouseId,
            //    InDetailList = inboundOrder.Details.Select(p => new WarehouseInDetail
            //    {
            //        InDetailID = p.DetailNumber,
            //        CommodityID = p.ProductId,
            //        StockType = 1,
            //        Amount = p.Qty
            //    }).ToList()
            //};

            //result.Add(JobConstants.JobDataMapRequestKeyName, JsonConvert.SerializeObject(request));

            return result;
        }
    }
}
