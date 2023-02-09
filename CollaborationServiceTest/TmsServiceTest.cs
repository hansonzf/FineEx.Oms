using Newtonsoft.Json;
using Oms.Application.Contracts.CollaborationServices.Tms;
using Oms.Application.Contracts.CollaborationServices.Wms;
using Oms.Application.Orders;
using Oms.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tms.Client;
using Wms.Client;

namespace CollaborationServiceTest
{
    public class TmsServiceTest
    {
        [Fact]
        public async Task Test_dispatch_order()
        {
            var srv = new TmsService("http://web-test.tms.fineyun.cn/api", "", "");

            var resp = await srv.DispatchOrdersAsync(BusinessTypes.Transport, GetTransportOrderTestData(), null);
        }

        List<TransportOrderDto> GetTransportOrderTestData()
        {
            return new List<TransportOrderDto> {
                new TransportOrderDto
                {
                    Id = new Guid("57fafecd-80fe-7f40-d747-3a093ca2daa6"),
                    OrderNumber = "TR202302070200224385",
                    ExternalOrderNumber = "123123",
                    OrderSource = 1,
                    ReceivedAt = DateTime.Now,
                    ExpectingCompleteTime = DateTime.Now,
                    FactCompleteTime = DateTime.Now,
                    TransportId = 638113752224385906,
                    IsReturnBack = false,
                    TransportType = TransportTypes.Transfer,
                    Sender = new AddressDescription("3a091cec-a9fe-46e5-6c79-c7df457b6d28", "小程", "13872620358", "小程仓库", "湖北省", "武汉市", "江夏区", "湖北省武汉市江夏区小程仓库"),
                    Receiver = new AddressDescription("3a091cf2-ac01-a479-4ee2-436a9576e1d5", "小程", "13872620358", "小程门店", "上海", "上海市", "黄浦区", "上海上海市黄浦区小程门店"),
                    BusinessType = BusinessTypes.Transport,
                    ConsignState = ConsignStatus.Dispatching,
                    ExpectingPickupTime = DateTime.Now,
                    FactPickupTime = DateTime.Now,
                    Customer = new CustomerDescription(1, "小程测试客户"),
                    Details = new List<TransitCargo>
                    {
                        new TransitCargo(new Guid("57fafecd-80fe-7f40-d747-3a093ca2daa6"), "", "11111", 1, 0, 0, 0, 0)
                    },
                    TransportDetails = new List<TransportReceipt>
                    {
                        new TransportReceipt { 
                            OrderType = 1, 
                            CarrierId = "3a091cef-0594-76d2-264b-d2f978f97f9c", 
                            CarrierName = "小程承运商",
                            FromAddressId = "",
                            FromAddressName = "",
                            ToAddressId = "",
                            ToAddressName = ""
                        }
                    }
                }
            };
        }
    }
}
