using Microsoft.AspNetCore.Mvc;
using AnotherNewsPlatform.MVC.Mappers;
using AnotherNewsPlatform.MVC.Models;
using AnotherNewsPlatform.Services.NewsService;
using AnotherNewsPlatform.MVC.Mappers.Articles;

namespace AnotherNewsPlatform.MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly DtoToArticlePreviewMapper _mapper;


        public NewsController(INewsService newsService, DtoToArticlePreviewMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }
        // GET: NewsController
        public async Task<IActionResult> Index()
        {
            var news = await _newsService.GetNewsAsync();
            var newsViewModel = news.Select(n => _mapper.ToArticlePreview(n)).ToList();
            return View(new ArticleMainPageModel
            {
                AllArticles = newsViewModel
            });
        }
        // GET: NewsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: NewsController/Aggregate
        public ActionResult Aggregate()
        {
            return View();
        }
        // POST: NewsController/ProcessAggregation
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessAggregation()
        {
            await _newsService.AggregateNews(HttpContext.RequestAborted);
            return Ok();
        }
    }
}
