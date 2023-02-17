using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Domain.Orders;
using Oms.Domain.Orders.Specifications;
using Oms.Domain.OutboundOrders;
using Oms.Domain.Processings;
using Volo.Abp.Application.Services;
using Volo.Abp.Uow;

namespace Oms.Application.Orders
{
    public class OutboundOrderAppService : ApplicationService, IOutboundOrderAppService
    {
        readonly IOutboundOrderRepository repository;
        readonly IProcessingRepository processingRepository;

        public OutboundOrderAppService(IOutboundOrderRepository repository, IProcessingRepository processingRepository)
        {
            this.repository = repository;
            this.processingRepository = processingRepository;
        }

        public async Task<ServiceResult> CreateOutboundOrderAsync(OutboundOrderDto orderDto)
        {
            try
            {
                Guid orderId = GuidGenerator.Create();
                orderDto.Details.ForEach(d => d.BindToOrder(orderId));
                long outboundId = DateTime.Now.Ticks; //IdGenerator.GetId();
                string orderNumber = DateTime.Now.ToString("OTyyyyMMddhhmmssffff");

                var order = new OutboundOrder(
                    orderId, 
                    outboundId, 
                    orderNumber, 
                    orderDto.ExternalOrderNumber, 
                    orderDto.TenantId, 
                    orderDto.Customer, 
                    orderDto.CargoOwner, 
                    orderDto.DeliveryInfo, 
                    orderDto.DeliveryType, 
                    orderDto.OutboundType, 
                    orderDto.Warehouse, 
                    orderDto.ExpectingOutboundTime, 
                    orderDto.ExpectingCompleteTime);
                order.AddOrRepalceProducts(orderDto.Details);
                var createdOrder = await repository.CreateAsync(order);

                return new ServiceResult
                {
                    Success = createdOrder is not null
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Success = false,
                    Error = ex,
                    Message = ex.Message
                };
            }
        }

        public async Task<DataResult<OutboundOrderDto>> GetOutboundOrderByIdAsync(Guid orderId)
        {
            var order = await repository.GetAsync(orderId);
            return new DataResult<OutboundOrderDto> 
            {
                Success = order is not null ? true : false,
                Data = order is not null ? ObjectMapper.Map<OutboundOrder, OutboundOrderDto>(order) : null,
                Message = order is not null ? string.Empty : $"The order not be found which Id {orderId}"
            };
        }

        public async Task<DataResult<OutboundOrderDto>> GetOutboundOrderByIdAsync(long outboundId)
        {
            var order = await repository.GetAsync(outboundId);
            return new DataResult<OutboundOrderDto>
            {
                Success = order is not null ? true : false,
                Data = order is not null ? ObjectMapper.Map<OutboundOrder, OutboundOrderDto>(order) : null,
                Message = order is not null ? string.Empty : $"The order not be found which Id {outboundId}"
            };
        }

        public async Task<ServiceResult> CombineOrdersAsync(long[] orderIds)
        {
            var ids = await repository.GetOrderUuidByOrderIdAsync(orderIds);
            return await CombineOrdersAsync(ids.ToArray());
        }

        public async Task<ServiceResult> CombineOrdersAsync(Guid[] orderIds)
        {
            if (orderIds is null || !orderIds.Any())
                return new ServiceResult
                { 
                    Success = false,
                    Message = $"{nameof(orderIds)} is required"
                };

            var orders = await repository.GetByIdsAsync(orderIds);
            // 计算从数据库中取出的order和参数是否存在差集
            // 如果存在说明部分指定的id在数据库中不存在
            // 此时，系统应该报告错误，不要继续执行
            var exceptIds = orders.Select(o => o.Id).ToList().Except(orderIds);
            if (exceptIds.Any())
                return new ServiceResult 
                { 
                    Success = false, 
                    Message = $"The {exceptIds.Select(i => i.ToString()).Aggregate((l, r) => $"{l}, {r}")} of orderIds are not exist in database"
                };

            var order = orders.First();
            var relateOrderIds = order.MergeOrders(orders.ToArray());

            return new ServiceResult
            {
                Success = true,
                Message = "Orders combine successful"
            };
        }

        public async Task<ServiceResult> UndoCombineAsync(long masterOrderId, long[] slaveOrderIds)
        {
            var masterUuid = await repository.GetOrderUuidByOrderIdAsync(new long[1] { masterOrderId });
            if (!masterUuid.Any())
                return new ServiceResult { Success = false, Message = "Not found master order" };
            var slaveUuids = await repository.GetOrderUuidByOrderIdAsync(slaveOrderIds);
            if (!slaveUuids.Any())
                return new ServiceResult { Success = false, Message = "Parameter 'slaveOrderIds' is empty" };

            return await UndoCombineAsync(masterUuid.First(), slaveUuids.ToArray());
        }

        public async Task<ServiceResult> UndoCombineAsync(Guid masterOrderId, Guid[] slaveOrderIds)
        {
            if (slaveOrderIds is null || !slaveOrderIds.Any())
                return new ServiceResult
                {
                    Success = false,
                    Message = $"{nameof(slaveOrderIds)} is required"
                };

            var masterOrder = await repository.GetAsync(masterOrderId);
            var slaveOrders = await repository.GetByIdsAsync(slaveOrderIds);

            var exceptIds = slaveOrders.Select(o => o.Id).ToList().Except(slaveOrderIds);
            if (exceptIds.Any())
                return new ServiceResult
                {
                    Success = false,
                    Message = $"The {exceptIds.Select(i => i.ToString()).Aggregate((l, r) => $"{l}, {r}")} of orderIds are not exist in database"
                };

            if (masterOrder is not null)
            {
                var relateOrderIds = masterOrder.UndoMergeOrders(slaveOrders.ToArray());
                return new ServiceResult
                {
                    Success = true,
                    Message = "Undo combine successful"
                };
            }
            else
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "The combination master could not be found"
                };
            }
        }

        public async Task<ServiceResult> SetCheckStockResultAsync(CheckstockResultDto result)
        {
            if (result is null)
                return new ServiceResult
                {
                    Success = false,
                    Message = "Argument 'result' is required"
                };

            using var uom = UnitOfWorkManager.Begin();

            if (result.PassedList is not null && result.PassedList.Any())
            {
                foreach (var i in result.PassedList)
                {
                    var order = await repository.GetAsync(i.OutBoundID);
                    if (order is null) continue;
                    var proc = await processingRepository.GetByOrderIdAsync(order.Id);
                    proc.Complete(ProcessingSteps.B2bCheckoutInventory);

                    var heldDetail = i.DetailList.Select(
                        d => new HeldStockResult
                        {
                            OutBoundID = i.OutBoundID,
                            DetailNumber = d.OrderDetailID,
                            HeldQty = d.PickAmount,
                            ProductId = d.CommodityID
                        });
                    order.SetCheckedInventoryResult(heldDetail);
                }
            }

            if (result.NotPassedList is not null && result.NotPassedList.Any())
            {
                foreach (var i in result.NotPassedList)
                {
                    var order = await repository.GetAsync(i.OutBoundID);
                    if (order is null) continue;
                    var proc = await processingRepository.GetByOrderIdAsync(order.Id);
                    proc.Complete(ProcessingSteps.B2bCheckoutInventory);

                    var emptyResult = order.Details.Select(
                        d => new HeldStockResult
                        {
                            OutBoundID = order.OutboundId,
                            DetailNumber = d.DetailNumber,
                            HeldQty = 0,
                            ProductId = d.ProductId
                        });
                    order.SetCheckedInventoryResult(emptyResult);
                }
            }

            await uom.CompleteAsync();
            return new ServiceResult
            {
                Success = true,
                Message = "OK"
            };
        }

        public async Task<ListResult<CombineDetailDto>> GetCombineDetail(Guid masterOrderId)
        {
            var orders = await repository.GetCombinedOrdersAsync(masterOrderId);
            if (orders.Any())
            {
                return new ListResult<CombineDetailDto>
                {
                    Items = orders.Select(
                        o => new CombineDetailDto
                        {
                            OrderNumber = o.OrderNumber,
                            ExternalOrderNumber = o.ExternalOrderNumber,
                            Details = o.Details.Select(d => new CombinedProductDto
                            {
                                SKU = d.SKU,
                                ProductBatch = d.ProductBatch,
                                RequiredQty = d.RequiredQty,
                                StockType = d.StockType
                            })
                        }),
                    Success = true
                };
            }
            else
            {
                return new ListResult<CombineDetailDto> 
                { 
                    Success = true,
                    Items = new List<CombineDetailDto>(),
                    Message = "Not found any orders"
                };
            }
        }

        public async Task<ListResult<OutboundOrderDto>> ListOrdersAsync(OutboundQueryDto query)
        {
            var spec = new OutboundQuerySpecification
            { 
                CargoOwnerId = query.ConsignorId,
                //CustomerId = query.CustomerId,
                CustomerOrderCode = query.CustomerOrderCode,
                DeliveryType = query.DeliveryType,
                OrderCode = query.OrderCode,
                OrderStatus = query.OrderStatus,
                StockAuditStatus = query.StockAuditStatus,
                TimeRange = query.TimeRange,
                TimeType = query.TimeType,
                TMSTransportCode = query.TMSTransportCode,
                WarehouseId = query.WarehouseId,
                WMSOutWarehouseCode = query.WMSOutWarehouseCode 
            };
            var orders = await repository.ListAsync(spec);
            var dtos = ObjectMapper.Map<IEnumerable<OutboundOrder>, IEnumerable<OutboundOrderDto>>(orders);

            return new ListResult<OutboundOrderDto> { 
                Items = dtos,
                Success = true
            };
        }
    }
}
