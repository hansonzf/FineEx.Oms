using Oms.Application.Contracts;
using Oms.Application.Contracts.CollaborationServices.ThreePL;
using Oms.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus;
using Volo.Abp.Uow;

namespace Oms.Application.Orders.MatchTransportLineForOrdersHandlers
{
    public class MatchForTransportOrderHandler
        : ILocalEventHandler<MatchTransportLineForOrdersEvent>, ITransientDependency
    {
        readonly IThreePLService dataService;
        readonly IOrderRepository repository;
        readonly IUnitOfWorkManager uom;


        public MatchForTransportOrderHandler(IThreePLService dataService, IOrderRepository repository, IUnitOfWorkManager uom)
        {
            this.dataService = dataService;
            this.repository = repository;
            this.uom = uom;
        }

        public async Task HandleEventAsync(MatchTransportLineForOrdersEvent eventData)
        {
            if (eventData.BusinessType != BusinessTypes.Transport) return;

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
            var order = await repository.GetAsync<TransportOrder>(orderId);
            if (order is null) return;

            var orderInfo = new OrderDeliveryInfo
            {
                BusinessType = 3,
                CustomerId = order.Customer.CustomerId,
                OrderId = order.TransportId,
                FromAddressId = order.SenderInfo.AddressId,
                FromAddressName = order.SenderInfo.AddressName,
                FromProvince = order.SenderInfo.Province,
                FromCity = order.SenderInfo.City,
                FromArea = order.SenderInfo.District,
                ToAddressId = order.ReceiverInfo.AddressId,
                ToAddressName = order.ReceiverInfo.AddressName,
                ToProvince = order.ReceiverInfo.Province,
                ToCity = order.ReceiverInfo.City,
                ToArea = order.ReceiverInfo.District,
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
                    resources.Add(new TransportResource
                    {
                        Index = index++,
                        ResourceId = item.LabelKey,
                        Name = item.Label
                    });
                    resources.Add(new TransportResource
                    {
                        Index = index++,
                        ResourceId = item.TipKey,
                        Name = item.TipKey
                    });
                }

                var strategy = new TransportStrategy(transPlan.PlanName, transPlan.Remark, 1, resources);
                order.SetMatchedResult(strategy);

                await uom.Current.SaveChangesAsync();
            }
        }
    }
}
