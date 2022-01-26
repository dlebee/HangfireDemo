using Hangfire.Console;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Tasks
{
    public class EchoTask
    {
        public void Run(PerformContext context, string message)
        {
            context?.WriteLine(ConsoleTextColor.Blue, $"Message requested to be echoed is {message}");
            context?.WriteLine(ConsoleTextColor.White, $"Message requested to be echoed is {message}");
            context?.WriteLine(ConsoleTextColor.Red, $"Message requested to be echoed is {message}");
            context?.WriteLine(ConsoleTextColor.Green, $"Message requested to be echoed is {message}");
            context?.WriteLine(ConsoleTextColor.Yellow, $"Message requested to be echoed is {message}");
            context?.WriteLine(ConsoleTextColor.DarkYellow, $"Message requested to be echoed is {message}");
        }
    }
}
