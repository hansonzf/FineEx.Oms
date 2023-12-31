﻿using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Domain.Orders;
using Oms.Domain.Processings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Oms.Application.Orders.MatchTransportLineForOrdersHandlers
{
    public class MatchForOutboundOrderHandler
        : ILocalEventHandler<MatchTransportLineForOrdersEvent>, ITransientDependency
    {
        readonly IThreePLService dataService;
        readonly IOrderRepository repository;
        readonly IUnitOfWorkManager uom;
        readonly IOrderOperationAppService operationService;


        public MatchForOutboundOrderHandler(IThreePLService dataService, IOrderRepository repository, IUnitOfWorkManager uom, IOrderOperationAppService operationService)
        {
            this.dataService = dataService;
            this.repository = repository;
            this.uom = uom;
            this.operationService = operationService;
        }

        public async Task HandleEventAsync(MatchTransportLineForOrdersEvent eventData)
        {
            if (eventData.BusinessType != BusinessTypes.OutboundWithTransport) return;

            if (eventData.OrderId.HasValue)
            {
                await AutomaticMatchTransportPlan(eventData.TenantId, eventData.OrderId.Value);
            }
            else
            {
                // 手动执行，可批量执行，通过数字id查找
                foreach (var id in eventData.OrderIds)
                {
                    Guid? orderid = await repository.GetOrderUuidByOrderIdAsync(id, eventData.BusinessType);
                    if (orderid == null)
                        continue;
                    await AutomaticMatchTransportPlan(eventData.TenantId, orderid.Value);
                }
            }
        }

        async Task AutomaticMatchTransportPlan(string tenantId, Guid orderId)
        {
            using var unitOfWork = uom.Begin();
            var order = await repository.GetAsync<OutboundOrder>(orderId);
            if (order is null) return;

            var warehouseAddr = await dataService.GetWarehouseAddress(tenantId, order.Warehouse);
            var orderInfo = new OrderDeliveryInfo
            {
                BusinessType = 1,
                CustomerId = order.Customer.CustomerId,
                OrderId = order.OutboundId,
                FromAddressId = warehouseAddr.AddressId,
                FromAddressName = warehouseAddr.AddressName,
                FromProvince = warehouseAddr.Province,
                FromCity = warehouseAddr.City,
                FromArea = warehouseAddr.District,
                ToAddressId = order.DeliveryInfo.AddressId,
                ToAddressName = order.DeliveryInfo.AddressName,
                ToProvince = order.DeliveryInfo.Province,
                ToCity = order.DeliveryInfo.City,
                ToArea = order.DeliveryInfo.District
            };

            var result = await dataService.GetTransPlan(tenantId, orderInfo);
            if (!result.Success) return;
            if (result.Data.Count == 1)
            {
                // matched only one transport strategy
                var transPlan = result.Data.First();
                List<TransportResource> resources = new List<TransportResource>();
                int index = 0;
                foreach (var item in transPlan.TransPlanDetails.OrderBy(d => d.Sort))
                {
                    if (!string.IsNullOrEmpty(item.LabelKey))
                    {
                        resources.Add(new TransportResource
                        {
                            Index = index++,
                            ResourceId = item.LabelKey,
                            Name = item.Label,
                            Type = TransportResourceTypes.LogisticsCenter
                        });
                    }

                    if (!string.IsNullOrEmpty(item.TipKey))
                    {
                        resources.Add(new TransportResource
                        {
                            Index = index++,
                            ResourceId = item.TipKey,
                            Name = item.Tip,
                            Type = TransportResourceTypes.Vendor
                        });
                    }
                }

                await operationService.SetMatchedTransportStrategyAsync(order.OutboundId, 1, BusinessTypes.OutboundWithTransport, transPlan.PlanName, transPlan.Remark, resources);
                await unitOfWork.CompleteAsync();
                //var strategy = new TransportStrategy(transPlan.PlanName, transPlan.Remark, 1, resources);
                //order.SetMatchedResult(strategy);
                //await uom.Current.SaveChangesAsync();
            }
        }
    }
}
