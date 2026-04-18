namespace AnotherNewsPlatform.App.Models
{
    public class ArticleMainPageModel
    {
        public IEnumerable<ArticlePreviewModel> AllArticles { get; set; } = [];
        public IEnumerable<ArticlePreviewModel>? HotArticles { get; set; }
    }
}
