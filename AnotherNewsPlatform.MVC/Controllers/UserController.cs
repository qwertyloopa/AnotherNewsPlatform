using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.MVC.Controllers
{
    public class UserController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
