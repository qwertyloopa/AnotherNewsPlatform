using AnotherNewsPlatform.Core.Exceptions;
using AnotherNewsPlatform.Services.UserService;
using AnotherNewsPlatform.TokenService;
using AnotherNewsPlatform.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(ITokenService tokenService, IUserService userService, ILogger<TokenController> logger) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAccessToken(LoginModel model, CancellationToken cancellationToken)
        {
            if (await userService.VerifyUserAsync(model.Email, model.Password, cancellationToken))
            {
                var tokenIdentity =  await userService.GetLoginDataAsync(model.Email, model.Password, cancellationToken);

                if (tokenIdentity != null)
                {
                    return Ok(new
                    {
                        AccessToken = tokenService.GenerateAccessToken(tokenIdentity),
                        //RefreshToken = tokenService.GenerateRefreshToken()
                    });
                }
                logger.LogError($"ClaimsIdentity for {model.Email} is null");
                throw new InternalServerErrorException("No user found");
            }
            return Unauthorized();
        }
    }
}
