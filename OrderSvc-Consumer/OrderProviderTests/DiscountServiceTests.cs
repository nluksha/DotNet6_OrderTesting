using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PactNet;
using PactNet.Infrastructure.Outputters;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using Xunit;
using Xunit.Abstractions;

namespace OrderProviderTests
{
    public class DiscountServiceTests : IDisposable
    {
        private bool disposed;
        private readonly ITestOutputHelper output;
        private readonly string serviceUri;

        public DiscountServiceTests(ITestOutputHelper output)
        {
            this.output = output;
            serviceUri = "https://localhost:7001";
        }

        [Fact]
        public void PactWithOrderSvcShouldBeVerified()
        {
            var config = new PactVerifierConfig
            {
                Verbose = true,
                ProviderVersion = "2.0.0",
                CustomHeaders = new Dictionary<string, string>
                    {
                        {"Content-Type", "application/json; charset=utf-8"}
                    },
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(output)
                }
            };

            new PactVerifier(config)
                .ServiceProvider("Discounts", serviceUri)
                .HonoursPactWith("Orders")
                .PactUri(@"c:/temp/OrderSvcConsumer/orders-discounts.json")
                .Verify();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DiscountServiceTests()
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
