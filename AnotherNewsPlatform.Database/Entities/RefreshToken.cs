using System.ComponentModel.DataAnnotations;

namespace AnotherNewsPlatform.Database.Entities;

public class RefreshToken
{
    [Key]
    public Guid Id { get; set; }
    
    public string Device { get; set; }
    //public required string Token { get; set; }
    public bool IsRevoked { get; set; }
    public bool IsExpired => DateTime.UtcNow > Expires;
    public DateTime? Expires { get; set; }
    public DateTime? Created { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}