using Newtonsoft.Json;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Volo.Abp.DependencyInjection;

namespace Oms.Application.Jobs
{
    public class CheckInventoryOutboundJobDataMapper : IJobDataMapper, ITransientDependency
    {
        public Dictionary<string, string> GrabJobData(IEnumerable<BusinessOrderDto> orders)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!orders.Any())
                return result;

            if (orders.First().BusinessType == BusinessTypes.OutboundWithTransport)
            {
                result = GrabOutboundOrder(orders.OfType<OutboundOrderDto>());
                result.Add(JobConstants.JobDataMapBusinessTypeKeyName, "1");
            }

            return result;
        }

        private Dictionary<string, string> GrabOutboundOrder(IEnumerable<OutboundOrderDto> outboundOrders)
        {
            var result = new Dictionary<string, string>();

            //var orders = outboundOrders.Select(o => new OrderInfoModel
            //{
            //    OutBoundID = o.OutboundId,
            //    MemberID = o.CargoOwner.CustomerId,
            //    WarehouseID = o.WarehouseId,
            //    DetailList = o.Details.Select(d => new OrderInfoDetailModel
            //    {
            //        CommodityID = d.ProductId,
            //        MemberID = o.CargoOwner.CustomerId,
            //        Amount = d.RequiredQty,
            //        OrderDetailID = d.DetailNumber,
            //        ProductBatch = d.ProductBatch,
            //        StockType = (int)d.StockType
            //    }).ToList()
            //});
            //var request = new SalesInventoryAuditRequest
            //{ 
            //    BusinessType = 2,
            //    ModelType = 2,
            //    OrderList = new List<OrderInfoModel>(orders)
            //};
            //result.Add(JobConstants.JobDataMapRequestKeyName, JsonConvert.SerializeObject(request));
            //result.Add(JobConstants.JobDataMapPartitionKeyName, orders.First().WarehouseID.ToString());

            return result;
        }
    }
}
