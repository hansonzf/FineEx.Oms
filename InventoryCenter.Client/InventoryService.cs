using Newtonsoft.Json;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices;
using Oms.Application.Contracts.CollaborationServices.Inventory;
using Oms.Application.Orders;
using RestSharp;
using Volo.Abp.DependencyInjection;

namespace InventoryCenter.Client
{
    public class InventoryService : IInventoryService, ITransientDependency
    {
        readonly RestClient client;
        readonly string baseUrl;
        readonly string apiKey;
        readonly string apiSecret;

        public InventoryService(string baseUrl, string apiKey, string apiSecret)
        {
            var options = new RestClientOptions(baseUrl);
            client = new RestClient(options);
            this.baseUrl = baseUrl;
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        public async Task<ServiceResult> CheckStock(string partitionId, IEnumerable<OutboundOrderDto> orders)
        {
            var requestOrders = orders.Select(o => new OrderInfoModel
            {
                OutBoundID = o.OutboundId,
                MemberID = o.CargoOwner.CargoOwnerId,
                WarehouseID = o.Warehouse.WarehouseId,
                DetailList = o.Details.Select(d => new OrderInfoDetailModel
                {
                    CommodityID = d.ProductId,
                    MemberID = o.CargoOwner.CargoOwnerId,
                    Amount = d.RequiredQty,
                    OrderDetailID = d.DetailNumber,
                    ProductBatch = d.ProductBatch,
                    StockType = (int)d.StockType
                }).ToList()
            });
            var request = new SalesInventoryAuditRequest
            {
                BusinessType = 2,
                ModelType = 2,
                OrderList = new List<OrderInfoModel>(requestOrders)
            };
            var body = JsonConvert.SerializeObject(request);
            string postUrl = $"{baseUrl}/MemberSales/SalesAudit";
            var url = SignUtility.GetPostURL(apiKey, apiSecret, partitionId, "fineex.inventory.member.sales.audit", postUrl, body);
            var req = new RestRequest(url, Method.Post);
            req.AddBody(body, "application/json");
            var resp = await client.PostAsync<BaseResponse>(req);
            return new ServiceResult { Success = resp.Flag, Message = resp.Message };
        }

        public async Task<ServiceResult> ReleaseStock(string partitionId, IEnumerable<OutboundOrderDto> orders)
        {
            var requestOrders = orders.Select(o => new OrderModel
            {
                OutBoundID = o.OutboundId,
                MemberID = o.CargoOwner.CargoOwnerId,
                WarehouseID = o.Warehouse.WarehouseId,
                OrderCode = o.OrderNumber,
                OperationType = 2
            });
            var request = new SaleInventoryFreeRequest
            {
                BusinessType = 2,
                OrderList = new List<OrderModel>(requestOrders)
            };
            var body = JsonConvert.SerializeObject(request);
            string postUrl = $"{baseUrl}/MemberSales/SalesReturn";
            var url = SignUtility.GetPostURL(apiKey, apiSecret, partitionId, "fineex.inventory.member.sales.return", postUrl, body);
            var req = new RestRequest(url, Method.Post);
            req.AddBody(body, "application/json");
            var resp = await client.PostAsync<BaseResponse>(req);
            return new ServiceResult { Success = resp.Flag, Message = resp.Message };
        }
    }
}
