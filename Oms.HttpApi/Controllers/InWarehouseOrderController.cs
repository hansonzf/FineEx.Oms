using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.OrderCenter;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Application.Contracts.Processings;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.Domain.Shared;
using Oms.HttpApi.Controllers;
using Oms.HttpApi.Models;
using Oms.HttpApi.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using static Oms.HttpApi.Models.RspInWarehouseOrderQuery;

namespace Oms.HttpApi
{
    [ApiController]
    [Route("api/inwarehouseorder")]
    public class InWarehouseOrderController : BaseController
    {
        readonly IInboundOrderAppService orderService;
        readonly IOrderOperationAppService operationService;
        readonly IThreePLService dataService;
        readonly IOrderCenterService orderCenterService;

        public InWarehouseOrderController(IInboundOrderAppService orderService, IOrderOperationAppService operationService, IThreePLService dataService, IOrderCenterService orderCenterService)
        {
            this.orderService = orderService;
            this.operationService = operationService;
            this.dataService = dataService;
            this.orderCenterService = orderCenterService;
        }

        /// <summary>
        /// 新增入库受理单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SaveInWarehouseOrder")]
        public async Task<RspModel> SaveInOrder(ReqInWarehouseOrderAdd request)
        {
            var dto = ObjectMapper.Map<ReqInWarehouseOrderAdd, InboundOrderDto>(request);
            var res = await orderService.CreateInboundOrderAsync(dto);
            
            return res ? RspModel.Success() : RspModel.Fail();
        }

        /// <summary>
        /// 根据系统原始订单号查询信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("GetInfoByOriginOrderCode")]
        public async Task<RspModel> GetInfoByOriginOrderCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return RspModel.Fail("参数Code为空");

            return RspModel.Success($"查询成功");
        }

        /// <summary>
        /// 入库受理单编辑信息查询
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetInWarehouseOrderEdit")]
        public async Task<RspModel> GetInWarehouseOrderEdit(long orderId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 入库受理单查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("GetInWarehouseOrder")]
        public async Task<RspModel<RspInWarehouseOrderQuery>> GetInWarehouseOrder(ReqInWarehouseOrderQuery request)
        {
            var result = await orderService.ListOrderAsync(new Application.Contracts.InboundOrders.InboundQueryDto());
            if (result.Success)
            {
                var items = result.Items.Select(o => new InWarehouseOrderQueryModel
                { 
                    InID = o.InboundId,
                    InCode = o.OrderNumber,
                    UpSyncID = o.ExternalOrderNumber,
                    SourceOrderCode = o.OriginDeliveryNumber,
                    CreateDate = o.ReceivedAt.ToString("yyyy/MM/dd hh:mm:ss"),
                    OrderStatusNew = (int)o.OrderState,
                    OrderStatusName = o.OrderState.GetDescription(),
                    OrderTranPlanId = 0,
                    OrderTranPlanName = o.MatchedTransportLineName,
                    CustomerName = o.Customer.CustomerName,
                    CargoOwnerName = o.CargoOwner.CargoOwnerName,
                    WarehouseName = o.Warehouse.WarehouseName,
                    //InWarehouseCode = "",
                    InType = (int)o.InboundType,
                    InTypeName = o.InboundType.GetDescription(),
                    SumbitNumber = o.Details.Sum(d => d.Qty),
                    ComfirmNumber = o.Details.Sum(d => d.ConfirmedQty),
                    TransOrderCode = o.TransportDetails.FirstOrDefault().TransOrderCode,
                    SenderUnit = o.DeliveryInfo.AddressName,
                    Sender = o.DeliveryInfo.Contact,
                    SenderPhone = o.DeliveryInfo.Phone,
                    SenderAddress = o.DeliveryInfo.Address,
                    ExpectDeliveryDate = o.ExpectingCompleteTime.ToString("yyyy/MM/dd hh:mm:ss"),
                    //InWarehouseDateTime = "",
                    OrderSourceId = 0,
                    OrderSource = ""
                }).ToList();
                var resp = new RspInWarehouseOrderQuery 
                { 
                    Flag = true,
                    Data = items
                };

                return RspModel.Success<RspInWarehouseOrderQuery>(resp);
            }
            return RspModel.Fail<RspInWarehouseOrderQuery>();
        }

        /// <summary>
        /// 订单详情接口
        /// </summary>
        /// <param name="reqInWarehouseOrderInfo"></param>
        /// <returns></returns>
        [HttpPost("GetInWarehouseOrderInfo")]
        public async Task<RspModel<InWarehouseOrderInfoModel>> GetInWarehouseOrderInfo(ReqInWarehouseOrderInfo reqInWarehouseOrderInfo)
        {
            var order = await orderService.GetOrderByIdAsync(reqInWarehouseOrderInfo.InId);
            var dto = ObjectMapper.Map<InboundOrderDto, InWarehouseOrderInfoModel>(order);
            //var resp = new RspInWarehouseOrderInfo { Flag = dto is not null, Data = dto };
            if (order is not null)
                return RspModel.Success<InWarehouseOrderInfoModel>(dto);
            else
                return RspModel.Fail<InWarehouseOrderInfoModel>("数据为空");
        }


        /// <summary>
        /// 入库类型枚举接口
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInWarehouseType")]
        public async Task<RspModel> GetInWarehouseType()
        {
            return RspModel.Success(new List<object>() {
                    new{
                        Key = 6,
                        Value = "B2B入库"
                    }
                });
        }


        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("OrderSubmit")]
        public async Task<RspModel> OrderSubmit(ReqInWarehouseOrderSubmit request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 订单下发
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("OrderSend")]
        public async Task<RspModel<RspInWarehouseOrder>> OrderSend(ReqInWarehouseOrder request)
        {
            var result = await operationService.DispatchOrdersAsync(CurrentUser.TenantId, request.OrderIds, (BusinessTypes)request.OrderType);
            return RspModel.Success<RspInWarehouseOrder>(new RspInWarehouseOrder());
        }

        /// <summary>
        /// 撤销下发
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CancelOrderSend")]
        public async Task<RspModel> CancelOrderSend(ReqInWarehouseOrder request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CancelOrder")]
        public async Task<RspModel> CancelOrder(ReqOmsInOrderSendTOTms request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("OrderImport")]
        public async Task<RspModel> OrderImport(IFormFile file)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("OrderExport")]
        public async Task<RspModel> OrderExport(ReqInWarehouseOrderQuery request)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 入库商品查询
        /// </summary>
        /// <returns></returns>
        [HttpPost("CommoditySignQuery")]
        public async Task<RspModel<RspInWarehouseOderCommodity>> CommoditySignQuery(ReqInCommodityQuery commodityQuery)
        {
            var ret = await orderCenterService.QueryProduct((int)commodityQuery.CargoOwnerId, commodityQuery.BarCode);

            if (!ret.Any())
                return RspModel.Fail<RspInWarehouseOderCommodity>("商品未找到");

            List<RspInWarehouseOderCommodity> commodity = new();
            foreach (var item in ret)
            {
                commodity.Add(new RspInWarehouseOderCommodity
                {
                    BarCode = item.BarCode,
                    CommodityCode = item.ProductCode,
                    CommodityID = item.ProductId,
                    CommodityName = item.ProductName,
                });
            }
            return RspModel.Success<RspInWarehouseOderCommodity>(commodity.FirstOrDefault());
        }
        /// <summary>
        /// 入库商品导入
        /// </summary>
        /// <returns></returns>
        [HttpPost("ImportCommodityQuery")]
        public async Task<RspModel<List<RspInWarehouseOderCommodity>>> CommodityQuery([FromForm] ReqCommodityFileQuery reqCommodityFileQuery)
        {
            throw new NotImplementedException();
        }
    }
}
