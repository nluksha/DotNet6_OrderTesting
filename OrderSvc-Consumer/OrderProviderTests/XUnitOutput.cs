using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;
using IOutput = PactNet.Infrastructure.Outputters.IOutput;

namespace OrderProviderTests
{
    public class XUnitOutput : IOutput
    {
        private readonly ITestOutputHelper output;

        public XUnitOutput(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void Write(string message, OutputLevel level)
        {
            output.WriteLine(message);
        }

        public void WriteLine(string line)
        {
            output.WriteLine(line);
        }

        public void WriteLine(string message, OutputLevel level)
        {
            output.WriteLine(message);
        }
    }
}
