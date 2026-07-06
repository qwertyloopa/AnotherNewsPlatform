using Microsoft.AspNetCore.Mvc;
using AnotherNewsPlatform.MVC.Mappers;
using AnotherNewsPlatform.MVC.Models;
using AnotherNewsPlatform.Services.NewsService;
using Microsoft.AspNetCore.Authorization;
using AnotherNewsPlatform.MVC.Models.Articles;
using Microsoft.Extensions.Logging;

namespace AnotherNewsPlatform.MVC.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ArticleMapper _mapper;
        private readonly ILogger<NewsController> _logger;
        private readonly IAggregateNewsService _aggregateNewsService;


        public NewsController(INewsService newsService, IAggregateNewsService aggregateService, ArticleMapper mapper, ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _mapper = mapper;
            _logger = logger;
            _aggregateNewsService = aggregateService;
        }
        // GET: NewsController
        public async Task<IActionResult> Index( CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 15)
        {
            _logger.LogInformation("NewsController.Index called - retrieving news articles");

            try
            {
                var news = await _newsService.GetNewsByPage(pageNumber, pageSize);
                // _logger.LogDebug("Retrieved {NewsCount} articles from news service", news.Count());

                var newsViewModel = news.Select(n => _mapper.ToPreviewModel(n)).ToList();
                var newsCount = await _newsService.GetNewsCountAsync(cancellationToken);
                _logger.LogInformation("Successfully mapped {NewsCount} articles to view models", newsViewModel.Count);

                return View(new ArticleMainPageModel
                {
                    AllArticles = newsViewModel,
                    Pagination = new ArticlePaginationModel
                    {
                        ActualPage = pageNumber,
                        PageCount = (int)Math.Ceiling((double)newsCount / pageSize),
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving news articles in NewsController.Index");
                throw;
            }
        }
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult> ArticleView(Guid id)
        {
            _logger.LogDebug("NewsController.Details called with id: {ArticleId}", id);
            var article = await _newsService.GetByIdAsync(id, HttpContext.RequestAborted);
            var articleviewModel = _mapper.ToViewModel(article);
            if (article == null)
            {
                return NotFound();
            }
            return View(articleviewModel);
        }
        // GET: NewsController/Aggregate
        [HttpGet]
        [Authorize]
        public ActionResult Aggregate()
        {
            _logger.LogInformation("NewsController.Aggregate called by user: {UserName}", User?.Identity?.Name);
            return View();
        }
        // POST: NewsController/ProcessAggregation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessAggregation()
        {
            _logger.LogInformation("Starting news aggregation process");

            try
            {
                await _aggregateNewsService.AggregateNews(HttpContext.RequestAborted);
                _logger.LogInformation("News aggregation completed successfully");

                return RedirectToAction("Index", "News");
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(ex, "News aggregation was cancelled");
                return RedirectToAction("Index", "News");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during news aggregation");
                throw;
            }
        }
    }
}
