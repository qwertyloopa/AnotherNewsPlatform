using AnotherNewsPlatform.MVC.Models;

namespace AnotherNewsPlatform.MVC.Services;

public interface IHoroscopeService
{
    Task<HoroscopeResponse?> GetTodaysHoroscopeAsync(string sign, CancellationToken cancellationToken = default);
}