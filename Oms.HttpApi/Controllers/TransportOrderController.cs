using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oms.Application.Contracts;
using Oms.Application.Contracts.TransportOrders;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.Domain.Shared;
using Oms.HttpApi.Controllers;
using Oms.HttpApi.Models;

namespace Oms.HttpApi
{
    [ApiController]
    [Route("api/transportorder")]
    public class TransportOrderController : BaseController
    {
        readonly ITransportOrderAppService orderService;
        readonly IOrderOperationAppService orderOperationService;

        public TransportOrderController(ITransportOrderAppService orderService, IOrderOperationAppService orderOperationService)
        {
            this.orderService = orderService;
            this.orderOperationService = orderOperationService;
        }

        /// <summary>
        /// 保存物流运输订单
        /// </summary>
        /// <param name="addOrder"></param>
        /// <returns></returns>
        [HttpPost("SaveTransportOrder")]
        public async Task<RspModel> SaveTransportOrder(ReqTransportAddOrder addOrder)
        {
            var dto = ObjectMapper.Map<ReqTransportAddOrder, TransportOrderDto>(addOrder);
            dto.TenantId = CurrentUser.TenantId;

            if (addOrder.OrderId > 0)
                await orderService.UpdateOrderAsync(dto);
            else
                await orderService.CreateOrderAsync(dto);
            
            return RspModel.Success();
        }

        
        /// <summary>
        /// 获取物流运输分页
        /// </summary>
        /// <param name="reqTransportOrderPage"></param>
        /// <returns></returns>
        [HttpPost("GetTransportOrderPage")]
        public async Task<RspModel<List<RspOmsTransportOrder>>> GetTransportOrderPage([FromBody] ReqTransportOrderPage reqTransportOrderPage)
        {
            var query = ObjectMapper.Map<ReqTransportOrderPage, TransportQueryDto>(reqTransportOrderPage);
            var result = await orderService.ListOrderAsync(query);
            if (result.Success)
            {
                var resp = result.Items.Select(i => new RspOmsTransportOrder { 
                    OrderId = i.TransportId,
                    OrderCode = i.OrderNumber,
                    SyncOrderCode = i.ExternalOrderNumber,
                    CreatedDate = i.ReceivedAt,
                    OrderStatus = (int)i.OrderState,
                    OrderStatusName = i.OrderState.GetDescription(),
                    //TransPlanId = 0,
                    TransPlanName = i.MatchedTransportLineName,
                    CustomerId = i.Customer.CustomerId,
                    CustomerName = i.Customer.CustomerName,
                    FromAddressName = i.Sender.AddressName,
                    FromAddress = i.Sender.Address,
                    FromContacter = i.Sender.Contact,
                    FromPhone = i.Sender.Phone,
                    FromAddressId = i.Sender.AddressId,
                    TotalAmount = i.Details.Sum(d => d.PackageCount),
                    TotalWeight = i.Details.Sum(d => (decimal)d.Weight),
                    TotalVolume = i.Details.Sum(d => (decimal)d.Volume),
                    TransportFee = i.Details.Sum(d => (double)d.Fee),
                    PlanPickDate = i.ExpectingPickupTime,
                    RealPickDate = i.FactPickupTime,
                    PlanDeliveryDate = i.ExpectingCompleteTime,
                    RealDeliveryDate = i.FactCompleteTime,
                    SignType = (int)i.ConsignState,
                    //PickImageNum = 0,
                    //IsBack = 0,
                    OrderType = (int)i.TransportType,
                    OrderTypeName = i.TransportType.GetDescription(),
                    SourceType = i.OrderSource,
                    CreatedBy = CurrentUser.UserId,

                }).ToList();
                return RspModel.Success<List<RspOmsTransportOrder>>(resp);
            }

            return RspModel.Fail<List<RspOmsTransportOrder>>();
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetTransportOrderById")]
        public async Task<RspModel<RspOrderDetail>> GetTransportOrderById(long orderId)
        {
            var res = await orderService.GetAsync(orderId);
            var order = new RspOrderDetail();
            if (res.Data is not null)
            {
                order.OrderId = res.Data.TransportId;
                order.OrderCode = res.Data.OrderNumber;
                order.SyncOrderCode = res.Data.ExternalOrderNumber;
                order.OrderStatus = (int)res.Data.ConsignState;
                order.OrderStatusName = EnumHelper.GetDescription(res.Data.ConsignState);
                //order.CustomerId = res.Data.Customer.CustomerId;
                order.CustomerName = res.Data.Customer.CustomerName;
                order.FromAddress = res.Data.Sender.Address;
                order.FromAddressName = res.Data.Sender.AddressName;
                order.FromContacter = res.Data.Sender.Contact;
                order.FromPhone = res.Data.Sender.Phone;
                order.FromAddressId = res.Data.Sender.AddressId;
                order.ToAddress = res.Data.Receiver.Address;
                order.ToAddressName = res.Data.Receiver.AddressName;
                order.ToContacter = res.Data.Receiver.Contact;
                order.ToPhone = res.Data.Receiver.Phone;
                order.ToAddressId = res.Data.Receiver.AddressId;
                order.OrderGoods = res.Data.Details.Select(c => new ReqOrderGoods { 
                    Amount = c.PackageCount,
                    DoubleAmount = c.InnerPackage,
                    GoodsCode = c.CargoCode,
                    LoadName = c.CargoName,
                    Volume = (decimal)c.Volume,
                    Weight = (decimal)c.Weight
                }).ToList();
                order.TransOrders = ObjectMapper.Map<List<TransportReceipt>, List<TransOrder>>(res.Data.TransportDetails);
            }

            return RspModel.Success<RspOrderDetail>(order);
        }

        
        /// <summary>
        /// 获取订单箱唛数据
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetPackageInfo")]
        public async Task<RspModel<List<RspPackageInfo>>> GetPackageInfo(long orderId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 订单下发
        /// </summary>
        /// <param name="reqTransOrder"></param>
        /// <returns></returns>
        [HttpPost("IssueTransportOrder")]
        public async Task<RspModel> IssueTransportOrder(ReqTransportOrder reqTransOrder)
        {
            try
            {
                string tenantId = CurrentUser.TenantId;
                await orderOperationService.DispatchOrdersAsync(tenantId, reqTransOrder.OrderIds, BusinessTypes.Transport);
                return RspModel.Success();
            }
            catch
            {
                return RspModel.Fail();
            }
        }
        
        /// <summary>
        /// 撤销下发
        /// </summary>
        /// <param name="reqTransOrder"></param>
        /// <returns></returns>
        [HttpPost("CancelIssueTransportOrder")]
        public async Task<RspModel> CancelIssueTransportOrder(ReqTransportOrder reqTransOrder)
        {
            try
            {
                string tenantId = CurrentUser.TenantId;
                await orderOperationService.UndoDispatchOrdersAsync(tenantId, reqTransOrder.OrderIds, BusinessTypes.Transport);
                return RspModel.Success();
            }
            catch
            {
                return RspModel.Fail();
            }
        }
        
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="reqTransOrder"></param>
        /// <returns></returns>
        [HttpPost("CancelTransportOrder")]
        public async Task<RspModel> CancelTransportOrder(ReqTransportOrder reqTransOrder)
        {
            try
            {
                string tenantId = CurrentUser.TenantId;
                await orderOperationService.CancelOrderAsync(reqTransOrder.OrderIds, BusinessTypes.Transport);
                return RspModel.Success();
            }
            catch
            {
                return RspModel.Fail();
            }
        }
        
        /// <summary>
        /// 导入物流运输订单
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("ImportTransportOrder")]
        public async Task<RspModel> ImportTransportOrder(IFormFile file)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// 导出物流运输订单
        /// </summary>
        /// <param name="reqTransportOrderPage"></param>
        /// <returns></returns>
        [HttpPost("ExportTransportOrder")]
        public IActionResult ExportTransportOrder(ReqTransportOrderPage reqTransportOrderPage)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取修改订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("GetUpdateTransOrderInfo")]
        public async Task<RspModel<RspUpdateTransOrderInfo>> GetUpdateTransOrderInfo(long orderId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取运输凭证
        /// </summary>
        /// <param name="transOrderId"></param>
        /// <returns></returns>
        [HttpGet("GetImageById")]
        public async Task<RspModel<List<string>>> GetImageById(long transOrderId)
        {
            throw new NotImplementedException();
        }
    }
}
