using System.Diagnostics;
using AnotherNewsPlatform.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using AnotherNewsPlatform.MVC.Models;

namespace AnotherNewsPlatform.MVC.Controllers;

public class HomeController(IHoroscopeService horoscopeService) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var signs = new[] { "aries", "taurus", "gemini", "cancer", "leo", "virgo", "libra", "scorpio", "sagittarius", "capricorn", "aquarius", "pisces" };
        var random = new Random();
        var randomSign = signs[random.Next(signs.Length)];

        var horoscope = await horoscopeService.GetTodaysHoroscopeAsync(randomSign, cancellationToken);
        ViewBag.Horoscope = horoscope;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
