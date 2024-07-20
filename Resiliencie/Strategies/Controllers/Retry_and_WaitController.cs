using Microsoft.AspNetCore.Mvc;
using Stragegies.Reactive.Strategies.Retry.Exercises;

namespace Stragegies.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Retry_and_WaitController : ControllerBase
    {
        [HttpGet("v7")]
        public IActionResult PruebaRetryv7(int age)
        {
            var retry = new RetryDoom();
            //V7
            retry.CallV7(age);
            return Ok();
        }


        [HttpGet("v8")]
        public IActionResult PruebaRetryv8(int age)
        {
            var retry = new RetryDoom();
       
            //V8
            retry.CallV8(age);
            return Ok();
        }

    }
}
