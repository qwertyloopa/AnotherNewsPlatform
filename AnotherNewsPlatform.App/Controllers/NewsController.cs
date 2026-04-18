using AnotherNewsPlatform.App.Extensions;
using AnotherNewsPlatform.NewsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace AnotherNewsPlatform.App.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var newsDtos = await _newsService.GetNewsAsync();
            var articleModels = newsDtos.ToArticleModels();
            return View(articleModels);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var newsDto = await _newsService.GetByIdAsync(id);

            if (newsDto == null)
            {
                return NotFound();
            }

            var articleModel = newsDto.ToArticleModel();
            return View(articleModel);
        }

        [HttpGet]
        public IActionResult Aggregate()
        {
            return View(new Models.CreateArticleModel());
        }

        [HttpPost]
        public async Task<IActionResult> ProcessAggregation()
        {
            await _newsService.AggregateNews(HttpContext.RequestAborted);
            return Ok();
        }
    }
}

