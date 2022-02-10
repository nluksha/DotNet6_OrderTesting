using OrderSvc_Consumer;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OrderSvc_Test
{
    public class DiscountSvcTests: IClassFixture<DiscountSvcMock>
    {
        private readonly IMockProviderService mockProviderService;
        private readonly string serviceUri;

        public DiscountSvcTests(DiscountSvcMock discountSvcMock)
        {
            mockProviderService = discountSvcMock.MockProviderService;
            serviceUri = discountSvcMock.ServiceUri;
            mockProviderService.ClearInteractions();
        }

        [Fact]
        public async Task GetDiscountAdjustmentAmount()
        {
            var discountModel = new DiscountModel
            {
                CustomerRaiting = 4.1
            };

            mockProviderService
                .Given("Rate")
                .UponReceiving("Given a customer rating, a adjustment discount amount will be return.")
                .With(new ProviderServiceRequest
                {
                    Method = HttpVerb.Post,
                    Path = "/discount",
                    Body = discountModel,
                    Headers = new Dictionary<string, object>
                    {
                        {"Content-Type", "application/json; charset=utf-8"}
                    }
                })
                .WillRespondWith(new ProviderServiceResponse
                {
                    Status = 200,
                    Headers = new Dictionary<string, object>
                    {
                        {"Content-Type", "application/json; charset=utf-8"}
                    },
                    Body = new DiscountModel
                    {
                        CustomerRaiting = 4.1,
                        AmountToDiscount = .41
                    }
                });

            var httpClient = new HttpClient();
            var response = await httpClient
                .PostAsJsonAsync($"{serviceUri}/discount", discountModel);
            var discountModelReturned = await response.Content.ReadFromJsonAsync<DiscountModel>();

            Assert.Equal(discountModelReturned.CustomerRaiting, discountModelReturned.CustomerRaiting);
        }
    }
}
