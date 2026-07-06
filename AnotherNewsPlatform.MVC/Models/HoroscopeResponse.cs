using System.Text.Json.Serialization;

namespace AnotherNewsPlatform.MVC.Models;

public class HoroscopeResponse
{
    [JsonPropertyName("sign")]
    public string Sign { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("horoscope")]
    public string Horoscope { get; set; } = string.Empty;
}