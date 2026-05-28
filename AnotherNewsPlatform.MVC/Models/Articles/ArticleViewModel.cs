namespace AnotherNewsPlatform.MVC.Models.Articles
{
    public class ArticleViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Source { get; set; } = string.Empty;
        public string OriginalUrl { get; set; }
    }
}
