using Newtonsoft.Json.Linq;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Volo.Abp.DependencyInjection;

namespace Oms.Application.Jobs
{
    public class MatchTransportLineJobDataMapper : IJobDataMapper, ITransientDependency
    {
        public Dictionary<string, string> GrabJobData(IEnumerable<BusinessOrderDto> orders)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!orders.Any())
                return result;

            switch (orders.First().BusinessType)
            {
                case BusinessTypes.OutboundWithTransport:
                    result = GrabOutboundOrder(orders.OfType<OutboundOrderDto>());
                    break;
                case BusinessTypes.InboundWithTransport:
                    result = GrabInboundOrder(orders.OfType<InboundOrderDto>());
                    break;
                case BusinessTypes.Transport:
                    result = GrabTransportOrder(orders.OfType<TransportOrderDto>());
                    break;
                default:
                    break;
            }

            return result;
        }

        private Dictionary<string, string> GrabOutboundOrder(IEnumerable<OutboundOrderDto> outboundOrders)
        {
            var result = new Dictionary<string, string>();

            var orderIds = outboundOrders.Select(o => o.Id);
            var order = outboundOrders.First();
            var parameter = new JObject(
                    new JProperty("fromId", order.Warehouse.WarehouseId),
                    new JProperty("toId", order.DeliveryInfo.AddressId),
                    new JProperty("orderIds", new JArray(orderIds))
                );
            //var orderParameter = outboundOrders.Select(o => new JObject(
            //    new JProperty("from", new JObject(
            //        new JProperty("fromId", o.WarehouseId))),
            //    new JProperty("to", new JObject(
            //        new JProperty("toId", o.DeliveryInfo.AddressId)))
            //    )).First();

            result.Add(JobConstants.JobDataMapRequestKeyName, parameter.ToString());

            return result;
        }

        private Dictionary<string, string> GrabInboundOrder(IEnumerable<InboundOrderDto> inboundOrders)
        {
            var result = new Dictionary<string, string>();

            var orderIds = inboundOrders.Select(o => o.Id);
            var order = inboundOrders.First();
            var parameter = new JObject(
                    new JProperty("fromId", order.DeliveryInfo.AddressId),
                    new JProperty("toId", order.Warehouse.WarehouseId),
                    new JProperty("orderIds", new JArray(orderIds))
                );
            //var orders = inboundOrders.Select(o => new JObject(
            //    new JProperty("from", new JObject(
            //        new JProperty("province", o.DeliveryInfo.Province),
            //        new JProperty("city", o.DeliveryInfo.City),
            //        new JProperty("district", o.DeliveryInfo.District))),
            //    new JProperty("to", new JObject(
            //        new JProperty("warehouseId", o.WarehouseId),
            //        new JProperty("warehouseName", o.WarehouseName),
            //        new JProperty("province", "湖北省"),
            //        new JProperty("city", "武汉市"),
            //        new JProperty("district", "东湖高新区")))
            //    ));
            //var orderArray = new JArray(orders);

            result.Add(JobConstants.JobDataMapRequestKeyName, parameter.ToString());

            return result;
        }

        private Dictionary<string, string> GrabTransportOrder(IEnumerable<TransportOrderDto> transportOrders)
        {
            var result = new Dictionary<string, string>();

            var orderIds = transportOrders.Select(o => o.Id);
            var order = transportOrders.First();
            var parameter = new JObject(
                    new JProperty("fromId", order.Sender.AddressId),
                    new JProperty("toId", order.Receiver.AddressId),
                    new JProperty("orderIds", new JArray(orderIds))
                );
            //var orders = transportOrders.Select(o => new JObject(
            //    new JProperty("from", new JObject(
            //        new JProperty("province", o.Sender.Province),
            //        new JProperty("city", o.Sender.City),
            //        new JProperty("district", o.Sender.District)),
            //    new JProperty("to", new JObject(
            //        new JProperty("province", o.Receiver.Province),
            //        new JProperty("city", o.Receiver.City),
            //        new JProperty("district", o.Receiver.District)))
            //    )
            //));
            //var ordersArray = new JArray(orders);

            result.Add(JobConstants.JobDataMapRequestKeyName, parameter.ToString());

            return result;
        }
    }
}
