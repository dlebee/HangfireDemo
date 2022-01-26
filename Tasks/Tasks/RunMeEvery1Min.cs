using Hangfire.Console;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks.Tasks
{
    public class RunMeEvery1Min
    {
        private static Random random;
        static RunMeEvery1Min()
        {
            random = new Random();
        }

        public async Task Yay(PerformContext performContext)
        {
            performContext?.WriteLine("Started " + nameof(RunMeEvery1Min));
            var randomMs = random.Next(5000, 10000);
            performContext?.WriteLine($"Will randomly be running for {randomMs} MS");
            await Task.Delay(randomMs);
            performContext?.WriteLine(ConsoleTextColor.Green, $"Completed!");
        }
    }
}
