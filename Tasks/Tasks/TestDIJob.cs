using Hangfire.Console;
using Hangfire.Server;
using Microsoft.Extensions.Logging;

namespace Tasks.Tasks
{
    public class AService
    {
        private readonly ILogger<AService> logger;

        public AService(ILogger<AService> logger)
        {
            this.logger = logger;
        }

        public string AMethod()
        {
            logger.LogInformation("Called AMethod");
            return "A";
        }
    }

    public class TestDIJob
    {
        private readonly AService service;

        public TestDIJob(AService service)
        {
            this.service = service;
        }

        public void Run(PerformContext context)
        {
            var fromServicee = service.AMethod();
            context.WriteLine("From service: " + fromServicee);
        }
    }
}
