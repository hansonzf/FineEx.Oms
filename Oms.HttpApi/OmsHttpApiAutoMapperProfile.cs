using AutoMapper;
using Oms.Application.Contracts.TransportOrders;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.Domain.Shared;
using Oms.HttpApi.Models;
using System.Net;

namespace Oms.HttpApi
{
    public class OmsHttpApiAutoMapperProfile : Profile
    {
        public OmsHttpApiAutoMapperProfile()
        {
            CreateMap<TransOrder, TransportReceipt>().ReverseMap();
        }
    }

    public class OmsHttpApiTransportOrderMapperProfile : Profile
    {
        public OmsHttpApiTransportOrderMapperProfile()
        {
            CreateMap<ReqTransportOrderPage, TransportQueryDto>();

            CreateMap<ReqOrderGoods, TransitCargo>()
                .ForMember(dest => dest.CargoCode, conf => conf.MapFrom(src => src.GoodsCode))
                .ForMember(dest => dest.CargoName, conf => conf.MapFrom(src => src.LoadName))
                .ForMember(dest => dest.PackageCount, conf => conf.MapFrom(src => src.Amount))
                .ForMember(dest => dest.InnerPackage, conf => conf.MapFrom(src => src.DoubleAmount));
            CreateMap<ReqTransportAddOrder, TransportOrderDto>()
                .ForMember(dest => dest.TransportId, conf => conf.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.TransportType, conf => conf.MapFrom(src => (TransportTypes)src.OperateType))
                .ForMember(dest => dest.ExpectingCompleteTime, conf => conf.MapFrom(src => src.PlanDeliveryDate))
                .ForMember(dest => dest.ExpectingPickupTime, conf => conf.MapFrom(src => src.PlanPickDate))
                .ForMember(dest => dest.Customer, conf => conf.MapFrom(src => new CustomerDescription(src.CustomerId, src.CustomerName)))
                .ForMember(dest => dest.OrderNumber, conf => conf.MapFrom(src => src.OrderCode))
                .ForMember(dest => dest.ExternalOrderNumber, conf => conf.MapFrom(src => src.SyncOrderCode))
                .ForMember(dest => dest.Details, conf => conf.MapFrom(src => src.OrderGoods))
                .ForMember(dest => dest.Sender, conf => conf.MapFrom(
                    src => new AddressDescription(
                        src.FromStopId,
                        src.FromContacter,
                        src.FromPhone,
                        src.FromAddressName,
                        src.FromProvince,
                        src.FromCity,
                        src.FromArea,
                        src.FromAddress)
                    ))
                .ForMember(dest => dest.Receiver, conf => conf.MapFrom(
                    src => new AddressDescription(
                        src.ToStopId,
                        src.ToContacter,
                        src.ToPhone,
                        src.ToAddressName,
                        src.ToProvince,
                        src.ToCity,
                        src.ToArea,
                        src.ToAddress
                    )));
        }
    }

    public class OmsHttpApiInboundOrderMapperProfile : Profile
    {
        public OmsHttpApiInboundOrderMapperProfile()
        {
            CreateMap<InProduct, CheckinProduct>()
                .ForMember(dest => dest.ProductId, conf => conf.MapFrom(src => src.CommodityID))
                .ForMember(dest => dest.SKU, conf => conf.MapFrom(src => src.BarCode))
                .ForMember(dest => dest.ProductBatch, conf => conf.MapFrom(src => src.BatchCode))
                .ForMember(dest => dest.ProductCode, conf => conf.MapFrom(src => src.CommodityCode))
                .ForMember(dest => dest.ProductName, conf => conf.MapFrom(src => src.CommodityName))
                .ForMember(dest => dest.StockType, conf => conf.MapFrom(src => (StockTypes)src.StockType))
                .ForMember(dest => dest.Qty, conf => conf.MapFrom(src => src.Qty));
            CreateMap<ReqInWarehouseOrderAdd, InboundOrderDto>()
                .ForMember(dest => dest.ExpectingCompleteTime, conf => conf.MapFrom(src => src.ExpectDeliveryDate))
                .ForMember(dest => dest.IsReturnOrder, conf => conf.MapFrom(src => src.IsGoBackOrder))
                .ForMember(dest => dest.OriginDeliveryNumber, conf => conf.MapFrom(src => src.OriginalOrderCode))
                .ForMember(dest => dest.ExternalOrderNumber, conf => conf.MapFrom(src => src.ErpSyncID))
                .ForMember(dest => dest.InboundType, conf => conf.MapFrom(src => (InboundTypes)src.InType))
                .ForMember(dest => dest.Customer, conf => conf.MapFrom(src => new CustomerDescription(src.CustomerId, src.CustomerName)))
                .ForMember(dest => dest.CargoOwner, conf => conf.MapFrom(src => new CargoOwnerDescription(src.MemberId, "")))
                // TODO:缺少interface warehouse id 和warehouse name
                .ForMember(dest => dest.Warehouse, conf => conf.MapFrom(src => new WarehouseDescription(src.WarehouseID, src.InterfaceWarehouseID, "")))
                .ForMember(dest => dest.DeliveryInfo, conf => conf.MapFrom(
                    src => new AddressDescription
                    (
                        src.FromAddressId,
                        src.FromContacter,
                        src.FromPhone,
                        src.FromAddressName,
                        src.FromProvince,
                        src.FromCity,
                        src.FromArea,
                        src.FromAddress
                    )))
                .ForMember(dest => dest.Details, conf => conf.MapFrom(src => src.GoodList));




            CreateMap<CheckinProduct, InGoods>()
                .ForMember(dest => dest.CommodityId, conf => conf.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.BarCode, conf => conf.MapFrom(src => src.SKU))
                .ForMember(dest => dest.CommodityName, conf => conf.MapFrom(src => src.ProductBatch))
                .ForMember(dest => dest.BatchCode, conf => conf.MapFrom(src => src.ProductBatch))
                .ForMember(dest => dest.ProductType, conf => conf.MapFrom(src => src.StockType.GetDescription()))
                .ForMember(dest => dest.ConfirmQty, conf => conf.MapFrom(src => src.ConfirmedQty));
            CreateMap<TransportReceipt, TransportInfo>()
                .ForMember(dest => dest.TransportOrderCode, conf => conf.MapFrom(src => src.TransOrderCode))
                .ForMember(dest => dest.TransOrderType, conf => conf.MapFrom(src => src.OrderType))
                //.ForMember(dest => dest.TransOrderTypeName, conf => conf.MapFrom(src => src.))
                .ForMember(dest => dest.TransOrderStatus, conf => conf.MapFrom(src => (int)src.OrderStatus))
                //.ForMember(dest => dest.TransOrderStatusName, conf => conf.MapFrom(src => src.OrderStatus.GetDescription()))
                .ForMember(dest => dest.FromAddress, conf => conf.MapFrom(src => src.FromAddressName))
                .ForMember(dest => dest.PickDate, conf => conf.MapFrom(src => src.RealPickDate.ToString("yyyy/MM/dd hh:mm:ss")))
                .ForMember(dest => dest.SignDate, conf => conf.MapFrom(src => src.RealDeliveryDate.ToString("yyyy/MM/dd hh:mm:ss")))
                .ForMember(dest => dest.CarrierName, conf => conf.MapFrom(src => src.CarrierName))
                .ForMember(dest => dest.DriverName, conf => conf.MapFrom(src => src.DriverName))
                .ForMember(dest => dest.DriverPhone, conf => conf.MapFrom(src => src.DriverPhone))
                .ForMember(dest => dest.PlateNumber, conf => conf.MapFrom(src => src.PlateNumber))
                .ForMember(dest => dest.VehicleTypeName, conf => conf.MapFrom(src => src.VehicleTypeName))
                .ForMember(dest => dest.TransportCode, conf => conf.MapFrom(src => src.TransportCode))
                .ForMember(dest => dest.TransPictureNum, conf => conf.MapFrom(src => src.TransPictureNum));
            CreateMap<InboundOrderDto, InWarehouseOrderInfoModel>()
                .ForMember(dest => dest.IsGoBackOrder, conf => conf.MapFrom(src => src.IsReturnOrder))
                .ForMember(dest => dest.ErpSyncID, conf => conf.MapFrom(src => src.ExternalOrderNumber))
                .ForMember(dest => dest.InCode, conf => conf.MapFrom(src => src.OrderNumber))
                .ForMember(dest => dest.InStatus, conf => conf.MapFrom(src => (int)src.OrderState))
                .ForMember(dest => dest.OrderStatusName, conf => conf.MapFrom(src => src.OrderState.GetDescription()))
                //.ForMember(dest => dest.WMSInWarehouseCode, conf => conf.MapFrom(src => src.IsReturnOrder))
                //.ForMember(dest => dest.SourceOrderCode, conf => conf.MapFrom(src => (int)src.OrderState))
                .ForMember(dest => dest.InType, conf => conf.MapFrom(src => (int)src.InboundType))
                .ForMember(dest => dest.InTypeName, conf => conf.MapFrom(src => src.InboundType.GetDescription()))
                .ForMember(dest => dest.ExpectDeliveryDate, conf => conf.MapFrom(src => src.ExpectingCompleteTime.ToString("yyyy/MM/dd hh:mm:ss")))
                .ForMember(dest => dest.FromAddressId, conf => conf.MapFrom(src => src.DeliveryInfo.AddressId))
                .ForMember(dest => dest.FromAddressName, conf => conf.MapFrom(src => src.DeliveryInfo.AddressName))
                //.ForMember(dest => dest.ToAddressId, conf => conf.MapFrom(src => src.Warehouse.Uuid))
                .ForMember(dest => dest.ToAddressName, conf => conf.MapFrom(src => src.Warehouse.WarehouseName))
                .ForMember(dest => dest.Sender, conf => conf.MapFrom(src => src.DeliveryInfo.Contact))
                .ForMember(dest => dest.SenderAddress, conf => conf.MapFrom(src => src.DeliveryInfo.Address))
                .ForMember(dest => dest.SenderArea, conf => conf.MapFrom(src => src.DeliveryInfo.District))
                .ForMember(dest => dest.SenderCity, conf => conf.MapFrom(src => src.DeliveryInfo.City))
                .ForMember(dest => dest.SenderPhone, conf => conf.MapFrom(src => src.DeliveryInfo.Phone))
                .ForMember(dest => dest.SenderProvince, conf => conf.MapFrom(src => src.DeliveryInfo.Province))
                //.ForMember(dest => dest.Consignee, conf => conf.MapFrom(src => src.IsReturnOrder))
                //.ForMember(dest => dest.ConsigneeAddress, conf => conf.MapFrom(src => src.IsReturnOrder))
                //.ForMember(dest => dest.ConsigneeArea, conf => conf.MapFrom(src => src.IsReturnOrder))
                //.ForMember(dest => dest.ConsigneeCity, conf => conf.MapFrom(src => src.IsReturnOrder))
                //.ForMember(dest => dest.ConsigneePhone, conf => conf.MapFrom(src => src.IsReturnOrder))
                //.ForMember(dest => dest.ConsigneeProvince, conf => conf.MapFrom(src => src.IsReturnOrder))
                .ForMember(dest => dest.GoodList, conf => conf.MapFrom(src => src.Details))
                //.ForMember(dest => dest.OpreationLogs, conf => conf.MapFrom(src => src.IsReturnOrder))
                .ForMember(dest => dest.TransportInfos, conf => conf.MapFrom(src => src.TransportDetails));
        }
    }

    public class OmsHttpApiOutboundOrderMapperProfile : Profile
    {
        public OmsHttpApiOutboundOrderMapperProfile()
        {
            CreateMap<ReqGetOutWarehouseOrderList, OutboundQueryDto>();

            CreateMap<OutboundOrderDto, RspGetOutWarehouseOrderList>()
                .ForMember(dest => dest.OrderId, conf => conf.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.MemberID, conf => conf.MapFrom(src => src.CargoOwner.CargoOwnerId))
                .ForMember(dest => dest.OrderCode, conf => conf.MapFrom(src => src.OrderNumber))
                .ForMember(dest => dest.MergeOrderQty, conf => conf.MapFrom(src => src.MergedOrderCount))
                .ForMember(dest => dest.CustomerOrderCode, conf => conf.MapFrom(src => src.ExternalOrderNumber))
                .ForMember(dest => dest.CreateDate, conf => conf.MapFrom(src => src.ReceivedAt.ToString("yyyy/MM/dd hh:mm:ss")))
                .ForMember(dest => dest.OrderStatusName, conf => conf.MapFrom(src => src.OrderState.GetDescription()))
                //.ForMember(dest => dest.StockAuditStatusName, conf => conf.MapFrom(src => src.CargoOwner.CargoOwnerId))
                //.ForMember(dest => dest.TranPlanName, conf => conf.MapFrom(src => src.OrderNumber))
                //.ForMember(dest => dest.OrderTranPlanID, conf => conf.MapFrom(src => src.MergedOrderCount))
                .ForMember(dest => dest.ConsignorName, conf => conf.MapFrom(src => src.CargoOwner.CargoOwnerName))
                .ForMember(dest => dest.WarehouseName, conf => conf.MapFrom(src => src.Warehouse.WarehouseName))
                .ForMember(dest => dest.WMSOutWarehouseCode, conf => conf.MapFrom(src => src.OrderNumber))
                .ForMember(dest => dest.OutWarehouseTypeName, conf => conf.MapFrom(src => src.OutboundType.GetDescription()))
                .ForMember(dest => dest.PlanOutWarehouseDate, conf => conf.MapFrom(src => src.ExpectingOutboundTime.ToString("yyyy/MM/dd hh:mm:ss")))
                .ForMember(dest => dest.ActualOutWarehouseDate, conf => conf.MapFrom(src => src.FactOutboundTime.ToString("yyyy/MM/dd hh:mm:ss")))
                .ForMember(dest => dest.SubmitQty, conf => conf.MapFrom(src => src.Details.Sum(d => d.RequiredQty)))
                .ForMember(dest => dest.ConfirmQty, conf => conf.MapFrom(src => src.Details.Sum(d => d.FactQty)))
                .ForMember(dest => dest.DeliveryTypeName, conf => conf.MapFrom(src => src.DeliveryType.GetDescription()))
                //.ForMember(dest => dest.TMSTransportCode, conf => conf.MapFrom(src => src.DeliveryType.GetDescription()))
                .ForMember(dest => dest.ConsigneeUnit, conf => conf.MapFrom(src => src.DeliveryInfo.AddressName))
                .ForMember(dest => dest.ConsigneeName, conf => conf.MapFrom(src => src.DeliveryInfo.Contact))
                .ForMember(dest => dest.ConsigneePhone, conf => conf.MapFrom(src => src.DeliveryInfo.Phone))
                .ForMember(dest => dest.ConsigneeAddress, conf => conf.MapFrom(src => src.DeliveryInfo.Address))
                //.ForMember(dest => dest.OrderSourceIdName, conf => conf.MapFrom(src => src.))
                .ForMember(dest => dest.ExpectDeliveryDate, conf => conf.MapFrom(src => src.ExpectingCompleteTime.ToString("yyyy/MM/dd hh:mm:ss")))
                .ForMember(dest => dest.ActSignDate, conf => conf.MapFrom(src => src.FactCompleteTime.HasValue ? src.FactCompleteTime.Value.ToString("yyyy/MM/dd hh:mm:ss") : string.Empty));


            CreateMap<Goods, CheckoutProduct>()
                .ForMember(dest => dest.ProductId, conf => conf.MapFrom(src => src.CommodityID))
                .ForMember(dest => dest.SKU, conf => conf.MapFrom(src => src.BarCode))
                .ForMember(dest => dest.ProductBatch, conf => conf.MapFrom(src => src.BatchCode))
                .ForMember(dest => dest.ProductCode, conf => conf.MapFrom(src => src.CommodityCode))
                .ForMember(dest => dest.ProductName, conf => conf.MapFrom(src => src.CommodityName))
                .ForMember(dest => dest.StockType, conf => conf.MapFrom(src => (StockTypes)src.StockType))
                .ForMember(dest => dest.RequiredQty, conf => conf.MapFrom(src => src.Qty));
            CreateMap<ReqOutWarehouseOderAdd, OutboundOrderDto>()
                .ForMember(dest => dest.Customer, conf => conf.MapFrom(
                    src => new CustomerDescription
                    (
                        src.CustomerId,
                        src.CustomerName
                    )))
                .ForMember(dest => dest.CargoOwner, conf => conf.MapFrom(
                    src => new CargoOwnerDescription
                    (
                        src.CargoowerId,
                        src.CargoowerName
                    )))
                .ForMember(dest => dest.DeliveryInfo, conf => conf.MapFrom(
                    src => new AddressDescription(
                        src.ConsigneeId,
                        src.Consignee,
                        src.ConsigneeAddress,
                        src.ConsigneePhone,
                        src.ConsigneeName,
                        src.ConsigneeProvinceAreaCity[0],
                        src.ConsigneeProvinceAreaCity[1],
                        src.ConsigneeProvinceAreaCity[2]
                    )))
                .ForMember(dest => dest.Details, conf => conf.MapFrom(src => src.CommodityInfoList))
                // TODO: 没有接口仓id
                .ForMember(dest => dest.Warehouse, conf => conf.MapFrom(src => new WarehouseDescription(src.WarehouseId, src.InterfaceWarehouseId, src.WarehouseIdName)))
                .ForMember(dest => dest.ExternalOrderNumber, conf => conf.MapFrom(src => src.CustomerOrderCode))
                .ForMember(dest => dest.ExpectingOutboundTime, conf => conf.MapFrom(src => DateTime.Parse(src.PlanOutWarehouseDate)))
                .ForMember(dest => dest.ExpectingCompleteTime, conf => conf.MapFrom(src => DateTime.Parse(src.ExpectDeliveryDate)))
                .ForMember(dest => dest.DeliveryType, conf => conf.MapFrom(src => (DeliveryTypes)src.DeliveryType))
                .ForMember(dest => dest.OutboundType, conf => conf.MapFrom(src => (OutboundTypes)src.OutWarehouseType));
        }
    }
}
