using AnotherNewsPlatform.Services.UserService;
using AnotherNewsPlatform.TokenService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(ITokenService tokenService, IUserService service, ILogger<TokenController> logger) : ControllerBase
    {

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetTokenData()
        //{

        //}
    }
}
