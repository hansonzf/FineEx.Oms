using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oms.Application.Contracts.CollaborationServices.OrderCenter;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using Oms.Domain.Shared;
using Oms.HttpApi.Controllers;
using Oms.HttpApi.Models;

namespace Oms.HttpApi
{
    [ApiController]
    [Route("api/outwarehouseorder")]
    public class OutWarehouseOrderController : BaseController
    {
        readonly IOutboundOrderAppService orderService;
        readonly IOrderOperationAppService operationService;
        readonly IThreePLService dataService;
        readonly IOrderCenterService orderCenterService;

        public OutWarehouseOrderController(
            IOutboundOrderAppService orderService, 
            IOrderOperationAppService operationService, 
            IThreePLService dataService, 
            IOrderCenterService orderCenterService)
        {
            this.orderService = orderService;
            this.operationService = operationService;
            this.dataService = dataService;
            this.orderCenterService = orderCenterService;
        }

        /// <summary>
        /// 新增出库受理单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<RspModel> AddOutWarehouseOrder(ReqOutWarehouseOderAdd req)
        {
            var dto = ObjectMapper.Map<ReqOutWarehouseOderAdd, OutboundOrderDto>(req);
            var result = await orderService.CreateOutboundOrderAsync(dto);

            return RspModel.Success();
        }

        /// <summary>
        /// 查询出库状态枚举列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetEnumList")]
        public async Task<RspModel> GetOutWarehouseOrderEnumList()
        {
            var ret = new List<List<RspGetOutWarehouseEnumList>>();
            Action<Type> getEnumListAction = type =>
            {
                var curEnumList = new List<RspGetOutWarehouseEnumList>();
                var enumList = Enum.GetValues(type);
                foreach (var item in enumList)
                {
                    curEnumList.Add(new RspGetOutWarehouseEnumList
                    {
                        Value = item.GetHashCode(),
                        Text = Enum.GetName(type, item.GetHashCode())
                    });
                }
                ret.Add(curEnumList);
            };
            //出库订单状态
            getEnumListAction(typeof(OutOrderStatusEnum));
            //库存审核状态
            getEnumListAction(typeof(StockAuditStatusEnum));
            //配送类型枚举
            getEnumListAction(typeof(DeliveryTypeEnum));
            //出库类型枚举
            getEnumListAction(typeof(OutWarehouseTypeEnum));
            //良次品类型枚举
            getEnumListAction(typeof(StockTypeEnum));
            //入库订单状态
            getEnumListAction(typeof(InOrderStatusEnum));
            //运输订单状态
            getEnumListAction(typeof(TransOrderStatusEnum));
            return RspModel.Success(ret);
        }

        [HttpPost("GetOrderList")]
        public async Task<RspModel<List<RspGetOutWarehouseOrderList>>> GetOutWarehouseOrderList(ReqGetOutWarehouseOrderList req)
        {
            var query = ObjectMapper.Map<ReqGetOutWarehouseOrderList, OutboundQueryDto>(req);
            var result = await orderService.ListOrdersAsync(query);
            if (result.Success)
            {
                var res = ObjectMapper.Map<IEnumerable<OutboundOrderDto>, IEnumerable<RspGetOutWarehouseOrderList>>(result.Items);
                return RspModel.Success<List<RspGetOutWarehouseOrderList>>(res.ToList());
            }

            return RspModel.Fail<List<RspGetOutWarehouseOrderList>>();
        }

        [HttpPost("Cancel")]
        public async Task<RspModel> CancelOutWarehouseOrder(ReqCancelOutWarehouseOrder req)
        {
            var result = await operationService.CancelOrderAsync(req.OutWarehouseOrderId, BusinessTypes.OutboundWithTransport);
            if (result.Success)
                return RspModel.Success();
            else
                return RspModel.Fail();
        }

        /// <summary>
        /// 导入出库单
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("Import")]
        public async Task<RspModel> ImportOutWarehouseOrder(IFormFile file)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 导出出库单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("Export")]
        public async Task<IActionResult> ExportOutWarehouseOrder(ReqExportOutWarehouseOrder req)
        {
            throw new NotImplementedException();
        }

        [HttpPost("GetDetail")]
        public async Task<RspModel> GetOutWarehouseOrderDetail(ReqGetOutWarehouseOrderDetail req)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 合并订单
        /// </summary>
        /// <returns></returns>
        [HttpPost("MergeOrder")]
        public async Task<RspModel> MergeOrder(ReqMergeOrder mergeOrderRequest)
        {
            var result = await orderService.CombineOrdersAsync(mergeOrderRequest.OrderIds.ToArray());
            if (result.Success)
                return RspModel.Success();
            else
                return RspModel.Fail();
        }
        /// <summary>
        /// 获取可合并订单
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetMergeOrder")]
        public async Task<RspModel<List<RspMergeOrder>>> GetMergeOrder(ReqMergeOrder mergeOrderRequest)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取子订单
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetChildOrder")]
        public async Task<RspModel<List<RspChildOrder>>> GetChildOrder(ReqGetChildOrder getChildOrderRequest)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 撤销合并
        /// </summary>
        /// <returns></returns>
        [HttpPost("UndoMerge")]
        public async Task<RspModel> UndoMerge(ReqIdList req)
        {
            throw new NotImplementedException();

        }
        /// <summary>
        /// 部分撤销合并
        /// </summary>
        /// <returns></returns>
        [HttpPost("PartUndoMergeOrder")]
        public async Task<RspModel> PartMergeOrder(ReqId req)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 订单下发
        /// </summary>
        /// <returns></returns>
        [HttpPost("OrderDistribution")]
        public async Task<RspModel<RspInWarehouseOrder>> OrderDistribution(ReqIdList req)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 取消订单下发
        /// </summary>
        /// <returns></returns>
        [HttpPost("CancelOrderDistribution")]
        public async Task<RspModel> CancelOrderDistribution(ReqIdList req)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 库存审核
        /// </summary>
        /// <returns></returns>
        [HttpPost("InventoryAudit")]
        public async Task<RspModel> InventoryAudit(ReqIdList req)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 出库商品查询
        /// </summary>
        /// <returns></returns>
        [HttpPost("CommodityQuery")]
        public async Task<RspModel<List<RspOutWarehouseOderCommodity>>> CommodityQuery(ReqCommodityQuery commodityQuery)
        {
            var ret = await orderCenterService.QueryProduct((int)commodityQuery.CargoOwnerId, commodityQuery.BarCode);

            if (!ret.Any())
                return RspModel.Fail<List<RspOutWarehouseOderCommodity>>("商品未找到");

            List<RspOutWarehouseOderCommodity> commodity = new();
            foreach (var item in ret)
            {
                commodity.Add(new RspOutWarehouseOderCommodity
                {
                    Barcode = item.BarCode,
                    BatchCode = item.ProductBatch,
                    CommodityCode = item.ProductCode,
                    CommodityID = item.ProductId,
                    CommodityName = item.ProductName,
                    Qty = commodityQuery.CountNum,
                    StockType = commodityQuery.StockType,
                    StockTypeName = ((StockTypes)commodityQuery.StockType).GetDescription(),
                });
            }
            return RspModel.Success<List<RspOutWarehouseOderCommodity>>(commodity);
        }
        /// <summary>
        /// 出库商品导入
        /// </summary>
        /// <returns></returns>
        [HttpPost("ImportCommodityQuery")]
        public async Task<RspModel<List<RspOutWarehouseOderCommodity>>> CommodityQuery([FromForm] ReqCommodityFileQuery reqCommodityFileQuery)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 客户查询（全量信息）
        /// </summary>
        /// <returns></returns>
        [HttpPost("CustomerQuery")]
        public async Task<RspModel<List<RspCustomer>>> CustomerQuery()
        {
            var customer = await dataService.GetCustomer(CurrentUser.TenantId);
            List<RspCustomer> rspCustomer = customer.Select(
                s => new RspCustomer { 
                    CustomerId = s.CustomerId, 
                    CustomerName = s.CustomerName 
                }).ToList();
            return RspModel.Success<List<RspCustomer>>(rspCustomer);
        }

        /// <summary>
        /// 查询客户货主树形（全量信息）
        /// </summary>
        /// <returns></returns>
        [HttpPost("CustomerCargoOwnerQuery")]
        public async Task<RspModel<List<RspCustomerCargo>>> CustomerCargoOwnerQuery()
        {
            var customerCargoOwner = await dataService.GetCustomerCargoOwner(CurrentUser.TenantId);
            List<RspCustomerCargo> result = new();
            foreach (var c in customerCargoOwner)
            {
                result.Add(new RspCustomerCargo
                {
                    CustomerId = c.Customer.CustomerId,
                    CustomerName = c.Customer.CustomerName,
                    CargoOwnerList = c.CargoOwners.Select(o => new RspCargoOwner { CargoOwnerId = o.CargoOwnerId, CargoOwnerName = o.CargoOwnerName }).ToList()
                });
            }

            return RspModel.Success<List<RspCustomerCargo>>(result);
        }

        /// <summary>
        /// 查询客户下的货主或全部货主（支持查全部）
        /// </summary>
        /// <returns></returns>
        [HttpPost("CargoOwnerQuery")]
        public async Task<RspModel<List<RspCargoOwner>>> CargoOwnerQuery(ReqCustomerCargoOwnerQuery reqCustomerCargoOwnerQuery)
        {
            //var resp = await dataService.GetCargoOwner(CurrentUser.TenantId, reqCustomerCargoOwnerQuery.CustomerId);
            //var result = resp.Select(c => new RspCargoOwner { CargoOwnerId = c.CargoOwnerId, CargoOwnerName = c.CargoOwnerName }).ToList();
            return RspModel.Success<List<RspCargoOwner>>(new List<RspCargoOwner>());
        }

        /// <summary>
        /// 获取出库单信息（修改操作回显时）
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOutWarehouseOrder")]
        public async Task<RspModel<RspOutOrderEditInfo.OutOrderEditInfoModel>> GetOutWarehouseOrder(long orderId)
        {
            throw new NotImplementedException();
        }
    }
}
