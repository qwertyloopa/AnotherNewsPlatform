namespace AnotherNewsPlatform.MVC.Models
{
    public class ArticleMainPageModel
    {
        public IEnumerable<ArticlePreviewModel> AllArticles { get; set; } = [];
        public IEnumerable<ArticlePreviewModel>? HotArticles { get; set; }
    }
}
