using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreePL.Client;

namespace CollaborationServiceTest
{
    public class ThreePLServiceTest
    {
        IHttpClientFactory HttpClientFactory
        {
            get
            {
                return new TestHttpClientFactory();
            }
        }

        [Fact]
        public async Task TestGetCustomer()
        {
            var srv = new ThreePLService(HttpClientFactory);

            var customers = await srv.GetCustomer("");

            Assert.NotNull(customers);
        }

        [Fact]
        public async Task TestGetCarrier()
        { 
            var src = new ThreePLService(HttpClientFactory);

            var carrier = await src.GetCarrier("");

            Assert.NotNull(carrier);
        }

        [Fact]
        public async Task TestGetLogisticsCenter()
        {
            var src = new ThreePLService(HttpClientFactory);

            var logistics = await src.GetLogisticsCenter("");

            Assert.NotNull(logistics);
        }

        [Fact]
        public async Task TestGetCargoOwner()
        {
            var src = new ThreePLService(HttpClientFactory);

            var cargoOwner = await src.GetCargoOwner("");

            Assert.NotNull(cargoOwner);
        }

        [Fact]
        public async Task TestGetRouteScheme()
        {
            var src = new ThreePLService(HttpClientFactory);

            var resp = await src.GetRouteScheme("", "3a08d5d2-4873-d7d2-5889-0bb9d216315d");

            Assert.NotNull(resp);
        }
    }

    public class TestHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient();
        }
    }
}
