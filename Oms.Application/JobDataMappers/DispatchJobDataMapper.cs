using Newtonsoft.Json;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Application.Contracts.CollaborationServices.Tms;
using Oms.Application.Contracts.CollaborationServices.Wms;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Oms.Application.Jobs
{
    public class DispatchJobDataMapper : IJobDataMapper, ITransientDependency
    {
        readonly IThreePLService service;

        public DispatchJobDataMapper(IThreePLService service)
        {
            this.service = service;
        }

        public Dictionary<string, string> GrabJobData(IEnumerable<BusinessOrderDto> orders)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!orders.Any())
                return result;

            switch (orders.First().BusinessType)
            {
                case BusinessTypes.OutboundWithTransport:
                    result = GrabOutboundOrder(orders.OfType<OutboundOrderDto>());
                    result.Add(JobConstants.JobDataMapBusinessTypeKeyName, "1");
                    break;
                case BusinessTypes.InboundWithTransport:
                    result = GrabInboundOrder(orders.OfType<InboundOrderDto>());
                    result.Add(JobConstants.JobDataMapBusinessTypeKeyName, "2");
                    break;
                case BusinessTypes.Transport:
                    result = GrabTransportOrder(orders.OfType<TransportOrderDto>());
                    result.Add(JobConstants.JobDataMapBusinessTypeKeyName, "3");
                    break;
                default:
                    break;
            }

            return result;
        }

        private Dictionary<string, string> GrabOutboundOrder(IEnumerable<OutboundOrderDto> outboundOrders)
        {
            //if (!outboundOrders.Any())
            //    throw new ArgumentNullException(nameof(outboundOrders));
            //List<LocationDto>? locations = service.GetAddress(new Contracts.CurrentUser()).Result?.Items;
            //if (locations is null || !locations.Any())
            //    throw new BusinessException(message: "获取地址列表失败");
            //var result = new Dictionary<string, string>();
            //var warehouseId = outboundOrders.First().WarehouseId.ToString();
            //var warehouseAddress = locations.FirstOrDefault(l => l.Id == warehouseId);
            //if (warehouseAddress is null)
            //    throw new BusinessException(message: $"获取仓库地址失败，仓库Id：{warehouseId}");

            //MapOutboundOrders(result, outboundOrders);

            //List<IssueToTmsRequest> transportOrders = new List<IssueToTmsRequest>();
            //foreach (var order in outboundOrders)
            //{
            //    order.TransportDetails.Select(ot => new IssueToTmsRequest {
            //        OrderId = order.OutboundId,
            //        OrderCode = order.OrderNumber,
            //        OperateType = 1,
            //        CustomerId = order.CargoOwner.CustomerId.ToString(),
            //        CustomerName = order.CargoOwner.CustomerName,

            //        FromAddressId = ot.FromId,
            //        FromProvince = locations.SingleOrDefault(l => l.Id == ot.FromId).Province,
            //        FromCity = locations.SingleOrDefault(l => l.Id == ot.FromId).City,
            //        FromArea = locations.SingleOrDefault(l => l.Id == ot.FromId).District,
            //        FromAddress = locations.SingleOrDefault(l => l.Id == ot.FromId).DetailAddress,
            //        FromAddressName = locations.SingleOrDefault(l => l.Id == ot.FromId).Name,
            //        FromContacter = locations.SingleOrDefault(l => l.Id == ot.FromId).Contact,
            //        FromPhone = locations.SingleOrDefault(l => l.Id == ot.FromId).ContactPhone,

            //        ToAddressId = ot.ToId,
            //        ToProvince = locations.SingleOrDefault(l => l.Id == ot.ToId).Province,
            //        ToCity = locations.SingleOrDefault(l => l.Id == ot.ToId).City,
            //        ToArea = locations.SingleOrDefault(l => l.Id == ot.ToId).District,
            //        ToAddress = locations.SingleOrDefault(l => l.Id == ot.ToId).DetailAddress,
            //        ToAddressName = locations.SingleOrDefault(l => l.Id == ot.ToId).Name,
            //        ToContacter = locations.SingleOrDefault(l => l.Id == ot.ToId).Contact,
            //        ToPhone = locations.SingleOrDefault(l => l.Id == ot.ToId).ContactPhone,

            //        OrderGoods = order.Details.Select(d => new ToTmsOrderGoods
            //        {
            //            Amount = d.RequiredQty,
            //            //GoodsCode = d.ProductCode,
            //            //LoadName = d.ProductName,
            //            DoubleAmount = 0
            //        }).ToList()
            //    });
            //    transportOrders.AddRange(transportOrders);
            //}

            //result.Add(JobConstants.JobDataMapDispatchToTmsKeyName, JsonConvert.SerializeObject(transportOrders));

            //return result;
            throw new NotImplementedException();
        }

        private Dictionary<string, string> GrabInboundOrder(IEnumerable<InboundOrderDto> inboundOrders)
        {
            //var result = new Dictionary<string, string>();

            //MapInboundOrders(result, inboundOrders);
            //var tmsPayload = inboundOrders.Select(o => new IssueToTmsRequest {
            //    UpOrderCode = o.OrderNumber,
            //    OperateType = o.IsReturnOrder ? 2 : 1,
            //    CustomerId = o.CargoOwner.CustomerId.ToString(),
            //    CustomerName = o.CargoOwner.CustomerName,

            //    FromAddressId = "",
            //    FromProvince = o.DeliveryInfo.Province,
            //    FromCity = o.DeliveryInfo.City,
            //    FromArea = o.DeliveryInfo.District,
            //    FromAddress = o.DeliveryInfo.Address,
            //    FromAddressName = o.DeliveryInfo.AddressName,
            //    FromContacter = o.DeliveryInfo.Contact,
            //    FromPhone = o.DeliveryInfo.Phone,

            //    OrderGoods = o.Details.Select(d => new ToTmsOrderGoods
            //    {
            //        Amount = d.Qty,
            //        //GoodsCode = d.ProductCode,
            //        //LoadName = d.ProductName,
            //        DoubleAmount = 0
            //    }).ToList()
            //});
            //result.Add(JobConstants.JobDataMapDispatchToTmsKeyName, JsonConvert.SerializeObject(tmsPayload));

            //return result;
            throw new NotImplementedException();
        }

        private Dictionary<string, string> GrabTransportOrder(IEnumerable<TransportOrderDto> transportOrders)
        {
            var result = new Dictionary<string, string>();
            MapTransportOrders(result, transportOrders);

            return result;
        }

        void MapOutboundOrders(Dictionary<string, string> parameters, IEnumerable<OutboundOrderDto> outboundOrders)
        {
            //var wmsPayload = new OutIssueToWmsRequest
            //{
            //    OutOrders = outboundOrders.Select(o => new OutIssueToWmsRequestDetail
            //    {
            //        UpSyncID = o.OutboundId,
            //        DownSyncID = o.OutboundId,
            //        MemberID = o.CargoOwner.CustomerId,
            //        //InterfaceWarehouseID
            //        //OutCode
            //        OrderSourceID = 102,
            //        //ErpSyncID
            //        //WayType
            //        //StoreCode
            //        WarehouseID = o.WarehouseId,
            //        OutType = (int)o.OutboundType,
            //        Consignee = o.DeliveryInfo.Contact,
            //        ConsigneePhone = o.DeliveryInfo.Phone,
            //        ConsigneeAddress = o.DeliveryInfo.Address,
            //        Province = o.DeliveryInfo.Province,
            //        City = o.DeliveryInfo.City,
            //        Area = o.DeliveryInfo.District,
            //        Memo = o.Remark,
            //        DetailList = o.Details.Select(d => new OutDetailRequest
            //        {
            //            DetailID = d.DetailNumber,
            //            CommodityID = d.ProductId,
            //            StockType = (int)d.StockType,
            //            ProductBatch = d.ProductBatch,
            //            Amount = d.RequiredQty
            //        }).ToList(),
            //        AuditDetailList = o.Details.Select(d => new OutAuditDetailRequest
            //        {
            //            CommodityID = d.ProductId,
            //            StockType = (int)d.StockType,
            //            PickAmount = d.HoldingQty,
            //            IsSpecifiedBatch = 0
            //        }).ToList()
            //    }).ToList()
            //};

            //if (wmsPayload.OutOrders.Any())
            //{
            //    parameters.Add(JobConstants.JobDataMapPartitionKeyName, wmsPayload.OutOrders.FirstOrDefault().WarehouseID.ToString());
            //    parameters.Add(JobConstants.JobDataMapDispatchToWmsKeyName, JsonConvert.SerializeObject(wmsPayload));
            //}
            throw new NotImplementedException();
        }

        void MapInboundOrders(Dictionary<string, string> parameters, IEnumerable<InboundOrderDto> inboundOrders)
        {
            //var wmsPayload = new InIssueToWmsRequest
            //{
            //    InOrders = inboundOrders.Select(o => new InIssueToWmsRequestDetail
            //    {
            //        UpSyncID = o.InboundId,
            //        DownSyncID = o.InboundId,
            //        //InCode
            //        MemberID = o.CargoOwner.CustomerId,
            //        //InterfaceWarehouseID
            //        WarehouseID = o.WarehouseId,
            //        //OrderSourceID
            //        InType = (int)o.InboundType,
            //        DetailList = o.Details.Select(d => new InOrderDetailList
            //        {
            //            DetailID = d.DetailNumber,
            //            CommodityID = d.ProductId,
            //            //StockType
            //            Amount = d.Qty,
            //        }).ToList()
            //    }).ToList()
            //};

            //if (wmsPayload.InOrders.Any())
            //{
            //    parameters.Add(JobConstants.JobDataMapPartitionKeyName, wmsPayload.InOrders.FirstOrDefault().WarehouseID.ToString());
            //    parameters.Add(JobConstants.JobDataMapDispatchToWmsKeyName, JsonConvert.SerializeObject(wmsPayload));
            //}
            throw new NotImplementedException();
        }

        void MapTransportOrders(Dictionary<string, string> parameters, IEnumerable<TransportOrderDto> transportOrders)
        {
            //var tmsPayload = transportOrders.Select(o => new IssueToTmsRequest {
            //    OrderCode = o.OrderNumber,
            //    OrderId = o.TransportId,
            //    OperateType = (int)o.TransportType,
            //    CustomerId = o.CargoOwner.CustomerId.ToString(),
            //    CustomerName = o.CargoOwner.CustomerName,

            //    FromAddressId = o.Sender.AddressId,
            //    FromProvince = o.Sender.Province,
            //    FromCity = o.Sender.City,
            //    FromArea = o.Sender.District,
            //    FromAddress = o.Sender.Address,
            //    FromAddressName = o.Sender.AddressName,
            //    FromContacter = o.Sender.Contact,
            //    FromPhone = o.Sender.Phone,

            //    ToAddressId = o.Receiver.AddressId,
            //    ToProvince = o.Receiver.Province,
            //    ToCity = o.Receiver.City,
            //    ToArea = o.Receiver.District,
            //    ToAddress = o.Receiver.Address,
            //    ToAddressName = o.Receiver.AddressName,
            //    ToContacter = o.Receiver.Contact,
            //    ToPhone = o.Receiver.Phone,

            //    OrderGoods = o.Details.Select(d => new ToTmsOrderGoods
            //    {
            //        Amount = d.PackageCount,
            //        //GoodsCode = d.Code,
            //        LoadName = d.CargoName,
            //        DoubleAmount = d.InnerPackage
            //    }).ToList()
            //});

            //parameters.Add(JobConstants.JobDataMapDispatchToTmsKeyName, JsonConvert.SerializeObject(tmsPayload));
            throw new NotImplementedException();
        }
    }
}
