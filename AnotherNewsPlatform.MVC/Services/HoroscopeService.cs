using System.Net.Http.Json;
using AnotherNewsPlatform.MVC.Models;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace AnotherNewsPlatform.MVC.Services;

public class HoroscopeService : IHoroscopeService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private const string BaseUrl = "https://ohmanda.com/api/horoscope";
    private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

    public HoroscopeService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<HoroscopeResponse?> GetTodaysHoroscopeAsync(string sign, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"horoscope_{sign}_{DateTime.UtcNow:yyyy-MM-dd}";

        if (_cache.TryGetValue(cacheKey, out HoroscopeResponse? cached))
            return cached;

        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{sign}/", cancellationToken);
            response.EnsureSuccessStatusCode();

            var horoscope = await response.Content.ReadFromJsonAsync<HoroscopeResponse>(cancellationToken: cancellationToken);

            if (horoscope is not null)
            {
                _cache.Set(cacheKey, horoscope, CacheDuration);
            }

            return horoscope;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Failed to fetch horoscope for sign {Sign}", sign);
            return null;
        }
    }
}