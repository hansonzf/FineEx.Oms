using InventoryCenter.Client;
using Newtonsoft.Json;
using Oms.Application.Contracts.CollaborationServices.Wms;
using Oms.Application.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wms.Client;

namespace CollaborationServiceTest
{
    public class WmsServiceTest
    {
        [Fact]
        public async Task Test_dispatch_outbound_order()
        {
            //var srv = new WmsService("http://10.32.30.10:9002/api", "", "");

            //string payload = JsonReaderUtility.ReadFileAsObject("WmsServiceTestPayload", "dispatch-outbound-order");
            //var request = JsonConvert.DeserializeObject<OutIssueToWmsRequest>(payload);

            //var resp = await srv.DispatchOutboundOrder("23409", request);

            //Assert.NotNull(resp);
            //Assert.True(resp.Flag);
        }

        [Fact]
        public async Task Test_dispatch_inbound_order()
        {
            //var srv = new WmsService("http://10.32.30.10:9002/api", "", "");

            //string payload = JsonReaderUtility.ReadFileAsObject("WmsServiceTestPayload", "dispatch-inbound-order");
            //var request = JsonConvert.DeserializeObject<InIssueToWmsRequest>(payload);

            //var resp = await srv.DispatchInboundOrder("23409", request);

            //Assert.NotNull(resp);
            //Assert.True(resp.Flag);
        }

        [Fact]
        public void TestMethod()
        {
            Expression<Func<OutboundOrderDto, bool>> e = a => true;
            e = e.And(t => t.Customer.CustomerName == "abc");
            e = e.And(t => t.Warehouse.WarehouseName == "ws");





            ParameterExpression p = Expression.Parameter(typeof(OutboundOrderDto), "o");


            Expression<Func<OutboundOrderDto, bool>> exp;
            //ParameterExpression p = Expression.Parameter(typeof(OutboundOrderDto), "o");
            MemberExpression owner = Expression.Property(p, "CargoOwner");
            var cstproperty = Expression.PropertyOrField(owner, "CustomerId");
            var ownerproperty = Expression.PropertyOrField(owner, "CargoOwnerId");

            var customerId = Expression.Constant(1);
            var ownerId = Expression.Constant(1);
            var code = Expression.Constant("abc");

            Expression<Func<OutboundOrderDto, bool>> c1 = o => o.CargoOwner.CargoOwnerId == 1;
            var condition1 = Expression.Equal(cstproperty, customerId);
            Expression<Func<OutboundOrderDto, bool>> c2 = o => o.CargoOwner.CargoOwnerId == 1;
            var condition2 = Expression.Equal(ownerproperty, ownerId);
            Expression<Func<OutboundOrderDto, bool>> c3 = o => o.CombinationCode == "abc";
            var codeExp = Expression.Property(p, "CombinationCode");
            var condition3 = Expression.Equal(codeExp, code);

            Expression predicate = Expression.AndAlso(condition1, condition2);
            predicate = Expression.AndAlso(predicate, condition3);

            exp = Expression.Lambda<Func<OutboundOrderDto, bool>>(predicate, new ParameterExpression[1] { p });
        }
    }

    public static class QueryBuilder
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> condition)
        {
            var inv = Expression.Invoke(condition, exp.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.And(exp.Body, inv), exp.Parameters);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> condition)
        {
            var inv = Expression.Invoke(condition, exp.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.Or(exp.Body, inv), exp.Parameters);
        }
    }
}
