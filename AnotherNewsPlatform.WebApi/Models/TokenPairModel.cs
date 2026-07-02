namespace AnotherNewsPlatform.WebApi.Models;

public class TokenPairModel
{
    public required string AccessToken { get; set; } 
    public Guid RefreshToken { get; set; }
}