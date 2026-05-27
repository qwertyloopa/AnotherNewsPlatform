using AnotherNewsPlatform.Services.NewsService;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController(ILogger<NewsController> logger, INewsService articleServie) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var article = await articleServie.GetByIdAsync(id, cancellationToken);
            return article == null ? NotFound() : Ok(article);
        }
    }
}
