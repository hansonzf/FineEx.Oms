using Microsoft.AspNetCore.Mvc;
using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.HttpApi.Controllers;
using Oms.HttpApi.Models;
using Volo.Abp.Users;

namespace Oms.HttpApi
{
    [ApiController]
    [Route("api/transportplan")]
    public class TransportPlanController : BaseController
    {
        readonly IOrderOperationAppService operationService;
        readonly IThreePLService dataService;

        public TransportPlanController(IOrderOperationAppService operationService, IThreePLService dataService)
        {
            this.operationService = operationService;
            this.dataService = dataService;
        }

        /// <summary>
        /// 查询匹配运输方案
        /// </summary>
        /// <param name="reqTransPlan"></param>
        /// <returns></returns>
        [HttpPost("GetMatchTransPlan")]
        public async Task<RspModel<RspMatchTransPlan>> GetMatchTransPlan(ReqTransPlan reqTransPlan)
        {
            var orderDto = await operationService.GetOrderByIdAsync(reqTransPlan.OrderId, (BusinessTypes)reqTransPlan.ReceiptType);
            if (orderDto == null)
                return RspModel.Fail<RspMatchTransPlan>("未找到指定的单据");
            var orderInfo = new OrderDeliveryInfo { OrderId = reqTransPlan.OrderId, BusinessType = reqTransPlan.ReceiptType };
            if (reqTransPlan.ReceiptType == 1)
            {
                OutboundOrderDto outboundOrder = orderDto as OutboundOrderDto;
                //var warehouseAddress = await dataService.GetWarehouseAddress(outboundOrder.Warehouse.Uuid);
                //orderInfo.CustomerId = outboundOrder.Customer.CustomerId;
                //orderInfo.FromProvince = warehouseAddress.Province;
                //orderInfo.FromCity = warehouseAddress.City;
                //orderInfo.FromArea = warehouseAddress.District;
                //orderInfo.ToAddressId = outboundOrder.DeliveryInfo.AddressId;
                //orderInfo.ToAddressName = outboundOrder.DeliveryInfo.AddressName;
                //orderInfo.ToProvince = outboundOrder.DeliveryInfo.Province;
                //orderInfo.ToCity = outboundOrder.DeliveryInfo.City;
                //orderInfo.ToArea = outboundOrder.DeliveryInfo.District;
            }
            if (reqTransPlan.ReceiptType == 2)
            {
                InboundOrderDto inboundOrder = orderDto as InboundOrderDto;
                //var warehouseAddress = await dataService.GetWarehouseAddress(inboundOrder.Warehouse.Uuid);
                //orderInfo.CustomerId = inboundOrder.Customer.CustomerId;
                //orderInfo.FromProvince = inboundOrder.DeliveryInfo.Province;
                //orderInfo.FromCity = inboundOrder.DeliveryInfo.City;
                //orderInfo.FromArea = inboundOrder.DeliveryInfo.District;
                //orderInfo.ToProvince = warehouseAddress.Province;
                //orderInfo.ToCity = warehouseAddress.City;
                //orderInfo.ToArea = warehouseAddress.District;
            }
            if (reqTransPlan.ReceiptType == 3)
            {
                TransportOrderDto transportOrder = orderDto as TransportOrderDto;
                orderInfo.CustomerId = transportOrder.Customer.CustomerId;
                orderInfo.FromAddressId = transportOrder.Sender.AddressId;
                orderInfo.FromAddressName = transportOrder.Sender.AddressName;
                orderInfo.FromProvince = transportOrder.Sender.Province;
                orderInfo.FromCity = transportOrder.Sender.City;
                orderInfo.FromArea = transportOrder.Sender.District;
                orderInfo.ToAddressId = transportOrder.Receiver.AddressId;
                orderInfo.ToAddressName = transportOrder.Receiver.AddressName;
                orderInfo.ToProvince = transportOrder.Receiver.Province;
                orderInfo.ToCity = transportOrder.Receiver.City;
                orderInfo.ToArea = transportOrder.Receiver.District;
            }
            

            if (orderInfo.CustomerId <= 0)
            {
                return RspModel.Fail<RspMatchTransPlan>("客户Id参数为空");
            }
            var result = await dataService.GetTransPlan(CurrentUser.TenantId, orderInfo);
            if (result.Success == false)
            {
                return RspModel.Fail<RspMatchTransPlan>(result.Message);
            }
            var matchInfo = new RspMatchTransPlan()
            {
                FromAddressName = orderInfo.FromAddressName,
                ToAddressName = orderInfo.ToAddressName,
                MatchTransPlans = result.Data
            };
            return RspModel.Success<RspMatchTransPlan>(matchInfo);
        }

        /// <summary>
        /// 编辑运输方案 查询详情
        /// </summary>
        /// <param name="reqTransPlan"></param>
        /// <returns></returns>
        [HttpPost("GetUpdateTransPlanInfo")]
        public async Task<RspModel<RspUpdatePlanInfo>> GetUpdateTransPlanInfo(ReqTransPlan reqTransPlan)
        {
            throw new NotImplementedException();
            //#region 查询匹配运输方案
            ////单据中心请求实体类
            //var reqAcceptInfo = new ReqOmsAcceptInfoQuery()
            //{
            //    OrderId = reqTransPlan.OrderId,
            //    ReceiptType = reqTransPlan.ReceiptType
            //};
            ////获取受理单详情
            //var acceptInfoRet = _omsHttpClientService.ExecuteQuery(reqAcceptInfo, reqTransPlan.MemberId.ToString());
            //if (!acceptInfoRet.Flag)
            //    return RspModel.Fail<RspUpdatePlanInfo>(acceptInfoRet.Message);
            //var orderInfo = acceptInfoRet.Data;
            //var customerId = Convert.ToInt32(orderInfo.CustomerId);
            //if (customerId <= 0)
            //{
            //    return RspModel.Fail<RspUpdatePlanInfo>("客户Id参数为空");
            //}
            //var result = await dataService.GetTransPlan(orderInfo, reqTransPlan.ReceiptType, orderInfo);
            //if (result.Flag == false)
            //{
            //    return RspModel.Fail<RspUpdatePlanInfo>(result.Message);
            //}
            //#endregion

            //#region 查询保存的运输方案
            //var newTransPlan = new TransportPlanRet()
            //{
            //    TransportPlanId = "0",
            //    PlanName = "手动调度",
            //    Remark = "",
            //    TransPlanDetails = new List<TransPlanDetail>
            //            {
            //                new TransPlanDetail
            //                {
            //                    Sort = 0,
            //                    LabelKey = "",
            //                    Label = orderInfo.FromAddressName,
            //                    TipKey = "",
            //                    Tip = orderInfo.ToAddressName
            //                }
            //            }
            //};
            //var transportPlanId = string.Empty;
            //var reqUpdatePlanInfo = new ReqOmsUpdatePlanInfoQuery()
            //{
            //    OrderId = reqTransPlan.OrderId,
            //    ReceiptType = reqTransPlan.ReceiptType
            //};
            ////获取保存的运输方案
            //var transPlanRet = _omsHttpClientService.ExecuteQuery(reqUpdatePlanInfo, reqTransPlan.MemberId.ToString());
            //if (transPlanRet.Flag)
            //{
            //    // 运输方案库Id
            //    transportPlanId = transPlanRet.TransportPlanId;
            //    if (string.IsNullOrEmpty(transportPlanId) || transportPlanId == "0")
            //    {
            //        newTransPlan.TransPlanDetails = transPlanRet.TransPlanInfo.TransPlanDetails;
            //    }
            //}

            //#endregion

            //var updatePlanInfo = new RspUpdatePlanInfo()
            //{
            //    TransportPlanId = transportPlanId,
            //    MatchTransPlans = result.Data,
            //    NewTransPlan = newTransPlan,
            //};
            //return RspModel.Success<RspUpdatePlanInfo>(updatePlanInfo);
        }

        /// <summary>
        /// 查看运输方案
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="receiptType"></param>
        /// <returns></returns>
        [HttpPost("GetTransPlanInfo")]
        public async Task<RspModel<TransportPlanRet>> GetTransPlanInfo(long orderId, int receiptType)
        {
            var transPlan = new TransportPlanRet();
            var reqUpdatePlanInfo = new ReqOmsUpdatePlanInfoQuery()
            {
                OrderId = orderId,
                ReceiptType = receiptType
            };

            var order = await operationService.GetOrderByIdAsync(orderId, (BusinessTypes)receiptType);
            if (order is null)
                return RspModel.Fail<TransportPlanRet>($"未找到Id为{orderId}的业务受理单");
            if (!order.TransportLineResources.Any())
                return RspModel.Fail<TransportPlanRet>("业务受理单还未匹配运输方案");

            var plan = new TransportPlanRet
            { 
                TransportPlanId = order.TransportStrategyId,
                PlanName = order.MatchedTransportLineName,
                Remark = order.TransportStrategyMemo,
                TransPlanDetails = order.TransportDetails
                    .Select(r => new TransPlanDetail { 
                        Sort = r.Index,
                        LabelKey = r.FromAddressId,
                        Label = r.FromAddressName,
                        TipKey = r.CarrierId,
                        Tip = r.CarrierName
                    }).ToList()
            };

            return RspModel.Success<TransportPlanRet>(plan);
        }

        /// <summary>
        /// 设置(修改)运输方案
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("SaveTranPlan")]
        public async Task<RspModel> SaveTranPlan(ReqSetTansScheme req)
        {
            var transportLine = req.TranPlanDetails.Select(p => new TransportResource
            {
                ResourceId = p.ResourceId,
                Name = p.ResourceName,
                Type = p.ResourceType,
                Province = p.Province,
                City = p.City,
                District = p.Area,
                Address = p.Address,
                Contact = p.Contacter,
                Phone = p.Phone
            });
            foreach (var id in req.OrderId)
            {
                await operationService.SetMatchedTransportStrategyAsync(id, req.MatchType, (BusinessTypes)req.ReceiptType, req.TranPlanName, req.TranPlanMemo, transportLine);
            }

            return RspModel.Success();
        }
    }
}
