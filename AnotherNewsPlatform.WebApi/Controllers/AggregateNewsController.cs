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
            RecurringJob.TriggerJob("AggregateNewsJob");
            return Accepted("AggregateNewsJob triggered");
        }

        /// <summary>
        /// Ручной запуск оценки тональности непрорешённых статей.
        /// </summary>
        [HttpPost("rate")]
        public IActionResult TriggerRating()
        {
            RecurringJob.TriggerJob("RateUnratedNewsJob");
            return Accepted("RateUnratedNewsJob triggered");
        }
    }
}