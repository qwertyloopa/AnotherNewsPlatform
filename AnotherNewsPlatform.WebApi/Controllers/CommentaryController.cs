using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentaryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(Guid ArticleId)
        {
            return Ok("Hello World!");
        }
        
    }
}
