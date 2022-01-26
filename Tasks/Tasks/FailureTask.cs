using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Filters;

namespace Tasks.Tasks
{
    public class FailureTask
    {
        [NotifyFailure]
        [AutomaticRetry(Attempts = 3, DelaysInSeconds = new int[] { 1, 2, 3 })]
        public void RunFailure(PerformContext context)
        {
            context?.WriteLine($"Task is gonna fail!");
            throw new Exception("I FAILED !!!");
        }
    }
}
