using AnotherNewsPlatform.Database;
using AnotherNewsPlatform.MVC.Models.User;
using AnotherNewsPlatform.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly AnpDbContext _context;
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(AnpDbContext context, ILogger<UserController> logger, IUserService userService)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
        }


        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginProcess(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogoutProcess()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterProcess(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Register", model);
        }

        [HttpGet, HttpPost]
        public IActionResult VerifyEmail(string email)
        {
            var emailInUse = _context.Users.Any(u => u.Email == email);

            if (emailInUse)
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}
