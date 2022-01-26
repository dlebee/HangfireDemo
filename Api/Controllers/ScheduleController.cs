using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tasks.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ScheduleController : Controller
    {
        [HttpGet("{message}")] 
        public IActionResult ExecuteEchoTaskNow(string message)
        {
            BackgroundJob.Enqueue<EchoTask>((instance) => instance.Run(default, message));
            return Ok();
        }

        [HttpGet("{message}")]
        public IActionResult ScheduleEchoTaskSoon(string message)
        {
            BackgroundJob.Schedule<EchoTask>((instance) => instance.Run(default, message), TimeSpan.FromSeconds(5));
            return Ok();
        }

        [HttpGet]
        public IActionResult ShowFailure()
        {
            BackgroundJob.Enqueue<FailureTask>(instance => instance.RunFailure(default));
            return Ok();
        }

        [HttpGet]
        public IActionResult DIJob()
        {
            BackgroundJob.Enqueue<TestDIJob>(instance => instance.Run(default));
            return Ok();
        }
    }
}
