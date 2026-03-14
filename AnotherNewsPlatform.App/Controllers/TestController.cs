using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.App.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Test()
        {
            string Text = "Hello, world!!";
            return Text;
        }
    }
}
