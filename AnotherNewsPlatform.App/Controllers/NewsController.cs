using AnotherNewsPlatform.NewsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AnotherNewsPlatform.App.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private CancellationToken token;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _newsService.GetNewsAsync();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Aggregate()
        {
            return View(new Models.CreateArticleModel());
        }

        public async Task<IActionResult> ProcessAggregation()
        {
            await _newsService.AggregateNewsAsync(token);
            return Ok();
        }
    }
}
