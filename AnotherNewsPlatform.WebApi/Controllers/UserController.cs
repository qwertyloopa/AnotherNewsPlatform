using AnotherNewsPlatform.Core.DTOs;
using AnotherNewsPlatform.Services.UserService;
using AnotherNewsPlatform.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Register(RegisterModel model, CancellationToken cancellationToken)
        {
            if(ModelState.IsValid)
            {
                await userService.RegisterAsync(model.Username, model.Email, model.Password, cancellationToken);
                return Ok();
            }
            return BadRequest(ModelState);
        }
        
        [HttpPatch("[action]/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ChangeUserData(UserDto user)
        {
            if (ModelState.IsValid)
            {
                await userService.UpdateUserAsync(user);
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
