using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Oms.Application.Contracts.CollaborationServices;
using Oms.Application.Contracts.CollaborationServices.OrderCenter;
using RestSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCenter.Client
{
    public class OrderCenterService : IOrderCenterService
    {
        readonly RestClient client;
        readonly string baseUrl;
        readonly string apiKey;
        readonly string apiSecret;

        public OrderCenterService(string baseUrl, string apiKey, string apiSecret)
        {
            var options = new RestClientOptions(baseUrl);
            client = new RestClient(options);
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        public async Task<IEnumerable<ProductDto>> QueryProduct(long cargoOwnerId, string barCode)
        {
            var body = JsonConvert.SerializeObject(new { MemberID = cargoOwnerId, BarCode = barCode });
            string postUrl = $"{baseUrl}/commodity/query";
            var url = SignUtility.GetPostURL(apiKey, apiSecret, cargoOwnerId.ToString(), "fineex.oms.commodity.query", postUrl, body);
            var req = new RestRequest(url, Method.Post);
            req.AddBody(body, "application/json");
            var resp = await client.PostAsync<CommodityQueryResponse>(req);

            if (resp.Data.Any())
            {
                return resp.Data.Select(r => new ProductDto { 
                    BarCode = r.BarCode,
                    ProductId = r.CommodityID,
                    ProductCode = r.CommodityCode,
                    ProductName = r.CommodityName,
                    ProductBatch = ""
                });
            }
            else
                return new List<ProductDto>();
        }
    }
}
