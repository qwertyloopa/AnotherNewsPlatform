namespace AnotherNewsPlatform.Database.Entities;

public class Commentary
{
    public Guid Id { get; set; }
    
    public Guid ArticleId { get; set; }
    public Article Article { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public string Text { get; set; }
    
    public DateTime CreatedDate { get; set; } =  DateTime.UtcNow;
}