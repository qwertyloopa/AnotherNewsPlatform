using Microsoft.Extensions.Configuration;

namespace AnotherNewsPlatform.Database.Entities;

public class RefreshToken()
{
    public Guid Id { get; set; }
    public string Device { get; set; }
    public bool IsExpired => DateTime.UtcNow > ExpiryTime;
    public bool IsRevoked { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    
    public DateTime ExpiryTime { get; set; }
    public DateTime CreationTime { get; set; }
    
}