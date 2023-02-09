using Newtonsoft.Json;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices;
using Oms.Application.Contracts.CollaborationServices.Wms;
using Oms.Application.Orders;
using RestSharp;

namespace Wms.Client
{
    public class WmsService : IWmsService
    {
        readonly RestClient client;
        readonly string baseUrl;
        readonly string apiKey;
        readonly string apiSecret;

        public WmsService(string baseUrl, string apiKey, string apiSecret)
        {
            var options = new RestClientOptions(baseUrl);
            client = new RestClient(options);
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        public async Task<ServiceResult> DispatchInboundOrdersAsync(string warehouseId, IEnumerable<BusinessOrderDto> orders)
        {
            var request = ConvertInboundIssueToWmsRequest(orders);
            var body = JsonConvert.SerializeObject(request);
            string url = $"{baseUrl}/app/upstream/in-order";
            var req = new RestRequest(url, Method.Post);
            req.AddBody(body, "application/json");
            var resp = await client.PostAsync<BaseResponse>(req);

            return new ServiceResult
            {
                Success = resp.Flag,
                Message = resp.Message
            };
        }

        public async Task<ServiceResult> DispatchOutboundOrdersAsync(string warehouseId, IEnumerable<BusinessOrderDto> orders)
        {
            var request = ConvertOutboundIssueToWmsRequest(orders);
            var body = JsonConvert.SerializeObject(request);
            string url = $"{baseUrl}/app/upstream/out-order";
            var req = new RestRequest(url, Method.Post);
            req.AddBody(body, "application/json");
            var resp = await client.PostAsync<BaseResponse>(req);

            return new ServiceResult
            {
                Success = resp.Flag,
                Message = resp.Message
            };
        }

        public async Task<ServiceResult> CancelOrder(long syncId, int memberId, int workerId, string cancelMemo)
        {
            var request = new OrderCancelRequest
            {
                UpSyncID = syncId,
                CancelMemo = cancelMemo,
                CancelMode = 2,
                CancelType = 2,
                MemberID = memberId,
                Operater = workerId,
                OperationDate = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")
            };
            var body = JsonConvert.SerializeObject(request);
            string url = $"{baseUrl}/api/app/upstream/order-cancel";
            var req = new RestRequest(url, Method.Post);
            req.AddBody(body, "application/json");
            var resp = await client.PostAsync<BaseResponse>(req);

            return new ServiceResult
            {
                Success = resp.Flag,
                Message = resp.Message
            };
        }

        OutIssueToWmsRequest ConvertOutboundIssueToWmsRequest(IEnumerable<BusinessOrderDto> orders)
        {
            var orderList = orders.OfType<OutboundOrderDto>().Select(o => new OutIssueToWmsRequestDetail
            {
                UpSyncID = o.OutboundId,
                DownSyncID = o.OutboundId,
                MemberID = o.CargoOwner.CargoOwnerId,
                InterfaceWarehouseID = o.Warehouse.InterfaceWarehouseId,
                OutCode = o.OrderNumber,
                OrderSourceID = 102,
                ErpSyncID = o.ExternalOrderNumber,
                //WayType
                //StoreCode
                WarehouseID = o.Warehouse.WarehouseId,
                OutType = (int)o.OutboundType,
                Consignee = o.DeliveryInfo.Contact,
                ConsigneePhone = o.DeliveryInfo.Phone,
                ConsigneeAddress = o.DeliveryInfo.Address,
                Province = o.DeliveryInfo.Province,
                City = o.DeliveryInfo.City,
                Area = o.DeliveryInfo.District,
                Memo = o.Remark,
                DetailList = o.Details.Select(d => new OutDetailRequest
                {
                    DetailID = d.DetailNumber,
                    CommodityID = d.ProductId,
                    StockType = (int)d.StockType,
                    ProductBatch = d.ProductBatch,
                    Amount = d.RequiredQty
                }).ToList(),
                AuditDetailList = o.Details.Select(d => new OutAuditDetailRequest
                {
                    CommodityID = d.ProductId,
                    StockType = (int)d.StockType,
                    PickAmount = d.HoldingQty,
                    IsSpecifiedBatch = 0
                }).ToList()
            });

            return new OutIssueToWmsRequest { OutOrders = orderList.ToList() };
        }

        InIssueToWmsRequest ConvertInboundIssueToWmsRequest(IEnumerable<BusinessOrderDto> orders)
        {
            var orderList = orders.OfType<InboundOrderDto>().Select(o => new InIssueToWmsRequestDetail
            {
                UpSyncID = o.InboundId,
                DownSyncID = o.InboundId,
                InCode = o.OrderNumber,
                MemberID = o.CargoOwner.CargoOwnerId,
                InterfaceWarehouseID = o.Warehouse.InterfaceWarehouseId,
                WarehouseID = o.Warehouse.WarehouseId,
                OrderSourceID = o.OrderSource,
                ErpSyncID = o.ExternalOrderNumber,
                InType = (int)o.InboundType,
                DetailList = o.Details.Select(d => new InOrderDetailList
                {
                    DetailID = d.DetailNumber,
                    CommodityID = d.ProductId,
                    StockType = (int)d.StockType,
                    ProductBatch = d.ProductBatch,
                    Amount = d.Qty,
                }).ToList()
            });

            return new InIssueToWmsRequest { InOrders = orderList.ToList() };
        }
    }
}
