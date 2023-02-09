using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.Tms;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using RestSharp;
using Volo.Abp.DependencyInjection;

namespace Tms.Client
{
    public class TmsService : ITmsService, ITransientDependency
    {
        readonly RestClient client;
        readonly string baseUrl = "http://web-test.tms.fineyun.cn/api";
        readonly string apiKey;
        readonly string apiSecret;

        public TmsService(string baseUrl, string apiKey, string apiSecret)
        {
            var options = new RestClientOptions(baseUrl);
            client = new RestClient(options);
            this.baseUrl = baseUrl;
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        public async Task<DataResult<IEnumerable<IssueToTmsResult>>> DispatchOrdersAsync(BusinessTypes businessType, IEnumerable<BusinessOrderDto> orders, IEnumerable<AddressDescription> warehouseAddresses)
        {
            IEnumerable<IssueToTmsRequest> request = Array.Empty<IssueToTmsRequest>();
            request = businessType switch
            { 
                BusinessTypes.Transport => ConvertTransportOrders(orders.OfType<TransportOrderDto>()),
                BusinessTypes.OutboundWithTransport => ConvertOutboundOrders(orders.OfType<OutboundOrderDto>(), warehouseAddresses),
                BusinessTypes.InboundWithTransport => ConvertInboundOrders(orders.OfType<InboundOrderDto>(), warehouseAddresses),
                _ => throw new ArgumentException($"Upsupported business of {Enum.GetName(businessType)}")
            };
            var body = JsonConvert.SerializeObject(request);
            string url = $"{baseUrl}/Order/AddOrder";
            var req = new RestRequest(url, Method.Post);
            req.AddBody(body, "application/json");
            var resp = await client.PostAsync(req);
            if (resp is not null && !string.IsNullOrEmpty(resp.Content))
            {
                var robj = JObject.Parse(resp.Content);
                if (robj == null)
                    return new DataResult<IEnumerable<IssueToTmsResult>> 
                    { 
                        Success = false,
                        Data = Array.Empty<IssueToTmsResult>()
                    };

                var successOrders = robj.SelectToken("Data.SuccessOrderList")
                    .Select(o => new IssueToTmsResult 
                    {
                        IsSuccess = true,
                        UpOrderCode = o["UpOrderCode"].Value<string>(),
                        TransOrderCode = o["TransOrderCode"].Value<string>() 
                    });
                var failedOrders = robj.SelectToken("Data.FailOrderList")
                    .Select(o => new IssueToTmsResult { 
                        IsSuccess = false,
                        UpOrderCode = o["OrderCode"].Value<string>()
                    });
                return new DataResult<IEnumerable<IssueToTmsResult>>
                { 
                    Success = true,
                    Data = successOrders.Concat(failedOrders)
                };
            }

            return new DataResult<IEnumerable<IssueToTmsResult>> { Success = false };
        }

        IEnumerable<IssueToTmsRequest> ConvertInboundOrders(IEnumerable<InboundOrderDto> orders, IEnumerable<AddressDescription> locations)
        {
            List<IssueToTmsRequest> transportOrders = new List<IssueToTmsRequest>();
            foreach (var order in orders)
            {
                order.TransportDetails.Select(ot => new IssueToTmsRequest
                {
                    OrderId = order.InboundId,
                    OrderCode = order.OrderNumber,
                    OperateType = order.IsReturnOrder ? 2 : 1,
                    CustomerId = order.Customer.CustomerId.ToString(),
                    CustomerName = order.Customer.CustomerName,

                    FromAddressId = ot.FromAddressId,
                    FromProvince = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.Province,
                    FromCity = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.City,
                    FromArea = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.District,
                    FromAddress = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.Address,
                    FromAddressName = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.AddressName,
                    FromContacter = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.Contact,
                    FromPhone = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.Phone,

                    ToAddressId = ot.ToAddressId,
                    ToProvince = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.Province,
                    ToCity = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.City,
                    ToArea = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.District,
                    ToAddress = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.Address,
                    ToAddressName = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.AddressName,
                    ToContacter = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.Contact,
                    ToPhone = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.Phone,

                    OrderGoods = order.Details.Select(d => new ToTmsOrderGoods
                    {
                        Amount = d.Qty,
                        GoodsCode = d.ProductCode,
                        LoadName = d.ProductName,
                        DoubleAmount = 0,
                        Volume = 0,
                        Weight = 0
                    }).ToList()
                });
                transportOrders.AddRange(transportOrders);
            }

            return transportOrders;
        }

        IEnumerable<IssueToTmsRequest> ConvertOutboundOrders(IEnumerable<OutboundOrderDto> orders, IEnumerable<AddressDescription> locations)
        {
            List<IssueToTmsRequest> transportOrders = new List<IssueToTmsRequest>();
            foreach (var order in orders)
            {
                order.TransportDetails.Select(ot => new IssueToTmsRequest
                {
                    OrderId = order.OutboundId,
                    OrderCode = order.OrderNumber,
                    OperateType = 1,
                    CustomerId = order.Customer.CustomerId.ToString(),
                    CustomerName = order.Customer.CustomerName,

                    FromAddressId = ot.FromAddressId,
                    FromProvince = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.Province,
                    FromCity = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.City,
                    FromArea = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.District,
                    FromAddress = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.Address,
                    FromAddressName = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.AddressName,
                    FromContacter = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.Contact,
                    FromPhone = locations.SingleOrDefault(l => l.AddressId == ot.FromAddressId)?.Phone,

                    ToAddressId = ot.ToAddressId,
                    ToProvince = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.Province,
                    ToCity = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.City,
                    ToArea = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.District,
                    ToAddress = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.Address,
                    ToAddressName = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.AddressName,
                    ToContacter = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.Contact,
                    ToPhone = locations.SingleOrDefault(l => l.AddressId == ot.ToAddressId)?.Phone,

                    OrderGoods = order.Details.Select(d => new ToTmsOrderGoods
                    {
                        Amount = d.RequiredQty,
                        GoodsCode = d.ProductCode,
                        LoadName = d.ProductName,
                        DoubleAmount = 0,
                        Volume = 0,
                        Weight = 0
                    }).ToList()
                });
                transportOrders.AddRange(transportOrders);
            }

            return transportOrders;
        }

        IEnumerable<IssueToTmsRequest> ConvertTransportOrders(IEnumerable<TransportOrderDto> orders)
        {
            List<IssueToTmsRequest> totalTransportTickets = new List<IssueToTmsRequest>();
            foreach (var order in orders)
            {
                var orderTransportTickets = order.TransportDetails.Select(t => new IssueToTmsRequest {
                    OrderCode = order.OrderNumber,
                    OrderId = order.TransportId,
                    OperateType = (int)order.TransportType,
                    CustomerId = order.Customer.CustomerId.ToString(),
                    CustomerName = order.Customer.CustomerName,

                    CarrierId = t.CarrierId,
                    CarrierName = t.CarrierName,

                    TotalAmount = order.Details.Sum(d => d.PackageCount),
                    TotalDoubleAmount = order.Details.Sum(d => d.InnerPackage),
                    TotalVolume = order.Details.Sum(d => (decimal)d.Volume),
                    TotalWeight = order.Details.Sum(d => (decimal)d.Weight),

                    FromAddressId = order.Sender.AddressId,
                    FromProvince = order.Sender.Province,
                    FromCity = order.Sender.City,
                    FromArea = order.Sender.District,
                    FromAddress = order.Sender.Address,
                    FromAddressName = order.Sender.AddressName,
                    FromContacter = order.Sender.Contact,
                    FromPhone = order.Sender.Phone,

                    ToAddressId = order.Receiver.AddressId,
                    ToProvince = order.Receiver.Province,
                    ToCity = order.Receiver.City,
                    ToArea = order.Receiver.District,
                    ToAddress = order.Receiver.Address,
                    ToAddressName = order.Receiver.AddressName,
                    ToContacter = order.Receiver.Contact,
                    ToPhone = order.Receiver.Phone,

                    OrderGoods = order.Details.Select(d => new ToTmsOrderGoods
                    {
                        Amount = d.PackageCount,
                        GoodsCode = d.CargoCode,
                        LoadName = d.CargoName,
                        DoubleAmount = d.InnerPackage,
                        Weight = (decimal)d.Weight,
                        Volume = (decimal)d.Volume
                    }).ToList()
                });
                totalTransportTickets.AddRange(orderTransportTickets);
            }

            return totalTransportTickets;
        }
    }
}
