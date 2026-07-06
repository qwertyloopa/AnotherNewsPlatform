using AnotherNewsPlatform.Services.NewsService;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregateNewsController(IAggregateNewsService aggregateNewsService) : ControllerBase
    {
        /// <summary>
        /// Ручной запуск агрегации новостей.
        /// </summary>
        [HttpPost("aggregate")]
        public IActionResult TriggerAggregation()
        {
            RecurringJob.Trigger("AggregateNewsJob");
            return Accepted("AggregateNewsJob triggered");
        }

        /// <summary>
        /// Ручной запуск оценки тональности непрорешённых статей.
        /// </summary>
        [HttpPost("rate")]
        public IActionResult TriggerRating()
        {
            RecurringJob.Trigger("RateUnratedNewsJob");
            return Accepted("RateUnratedNewsJob triggered");
        }
    }
}