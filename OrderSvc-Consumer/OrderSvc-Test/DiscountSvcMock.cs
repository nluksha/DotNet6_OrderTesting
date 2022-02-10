using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Mocks;
using PactNet.Mocks.MockHttpService;

namespace OrderSvc_Test
{
    public class DiscountSvcMock : IDisposable
    {
        private readonly IPactBuilder pactBuilder;
        private readonly int servicePort = 9222;
        private bool disposed = false;

        public IMockProviderService MockProviderService { get; }
        public string ServiceUri => $"http://localhost:{servicePort}";

        public DiscountSvcMock()
        {
            var pactConfig = new PactConfig
            {
                SpecificationVersion = "2.0.0",
                PactDir = @"c:/temp/OrderSvcConsumer",
                LogDir = @"c:/temp/OrderSvcConsumer/logs",
            };

            pactBuilder = new PactBuilder(pactConfig)
                .ServiceConsumer("Orders")
                .HasPactWith("Discounts");

            MockProviderService = pactBuilder.MockService(servicePort,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    pactBuilder.Build();
                }

                disposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DiscountSvcMock()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
