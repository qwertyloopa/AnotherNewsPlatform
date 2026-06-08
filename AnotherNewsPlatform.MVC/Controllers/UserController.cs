using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.MVC.Models.User;
using AnotherNewsPlatform.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AnotherNewsPlatform.MVC.Mappers;
using Microsoft.AspNetCore.Authorization;


namespace AnotherNewsPlatform.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly AnpDbContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly UserMapper _mapper;

        public UserController(AnpDbContext context, ILogger<UserController> logger, IUserService userService, UserMapper mapper)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginProcess(LoginModel model, CancellationToken token)
        {
            if(ModelState.IsValid)
            {
                if(await _userService.VerifyUserAsync(model.Email, model.Password, token))
                {
                    var claimsIdentity = await _userService.GetLoginDataAsync(model.Email, model.Password, token);
                    
                    if(claimsIdentity != null)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));   
                        return RedirectToAction("Index", "News");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid user");

                }
                ModelState.AddModelError(string.Empty, "Invalid email or password");
            }
            return NotFound();
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
            

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProcessing(RegisterModel model, CancellationToken token)
        {
            if(ModelState.IsValid)
            {
                await _userService.RegisterAsync(model.Username, model.Email, model.Password, token);
            }
            return View("Register", model);
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> VerifyEmail(string email, CancellationToken token)
        {
            bool isEmailExists = await _userService.VerifyEmailAsync(email, token);
            return Json(isEmailExists);
        }

        [HttpGet]
        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> Profile(string id)
        {
            long idToSearch = Convert.ToInt64(id);
            var user = await _userService.GetUserDtoAsync(idToSearch);
            var changeUserModel = _mapper.FromUserDtoToModel(user);
            return View(changeUserModel);
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeUserProcessing(ChangeUserModel model)
        {
            await _userService.UpdateUserAsync(_mapper.FromChangeUserModelToDto(model));
            return RedirectToAction("Index", "Home");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(long id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction("Logout", "Home");
        }
    }
}
