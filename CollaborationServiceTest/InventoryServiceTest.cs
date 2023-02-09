using InventoryCenter.Client;
using Newtonsoft.Json;

namespace CollaborationServiceTest
{
    public class InventoryServiceTest
    {
        [Fact]
        public async Task Test_checkstock()
        {
            //var srv = new InventoryService("http://172.16.100.22:8010/api/v1", "OMSD", "oms-ddd");

            //string payload = JsonReaderUtility.ReadFileAsObject("InventoryServiceTestPayload", "checkstockrequest");
            //var request = JsonConvert.DeserializeObject<SalesInventoryAuditRequest>(payload);

            //var resp = await srv.StockCheck("534201", request);
        }
    }
}