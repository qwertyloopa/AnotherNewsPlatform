using Microsoft.AspNetCore.Mvc;
using AnotherNewsPlatform.MVC.Mappers;
using AnotherNewsPlatform.Services.NewsService;
using AnotherNewsPlatform.MVC.Mappers.Articles;

namespace AnotherNewsPlatform.MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
        // GET: NewsController
        public async Task<IActionResult> Index()
        {
            var news = await _newsService.GetNewsAsync();
            return View();
        }

    }
}
