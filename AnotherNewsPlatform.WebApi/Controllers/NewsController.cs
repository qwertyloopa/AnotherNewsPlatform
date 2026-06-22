using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.WebApi.Mappers;
using AnotherNewsPlatform.WebApi.Models;
using AnotherNewsPlatform.Services.NewsService;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController(ILogger<NewsController> logger, INewsService? articleService, UserMapper mapper) : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var article = await articleService.GetByIdAsync(id, cancellationToken);
            return article == null ? NotFound() : Ok(new ArticleMapper().FromDtoToModel(article));
        }

        [HttpGet("{id}")]
        [ProducesResponseType<АrticleModel[]>(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetByRateAndSource(decimal? minRate, int? sourceId, CancellationToken cancellationToken)
        {
            var foundedArticles = articleService.GetNewsByRateAndSource(minRate, sourceId, cancellationToken);
            return Ok(foundedArticles);
        }
        
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateArticlePartially(Guid id, UpdateArticlePartsModel partsModel, CancellationToken cancellationToken)
        {
            var article = await articleService.GetByIdAsync(id, cancellationToken);
            if (article == null) return NotFound();
            await articleService.UpdatePartialArticleAsync(id, partsModel.Title, partsModel.Rate, cancellationToken: cancellationToken);
            return NoContent();
        }
        
        
    }
}
