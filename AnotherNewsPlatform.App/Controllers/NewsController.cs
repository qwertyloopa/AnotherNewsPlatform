using AnotherNewsPlatform.Services.NewsService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AnotherNewsPlatform.App.Controllers
{
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _newsService.GetNewsAsync();
            return View(articles);
        }

        public async Task<IActionResult> Aggregate()
        {
            return View();
        }
    }
}
