namespace AnotherNewsPlatform.MVC.Models.Articles;

public class ArticleMainPageModel
{
    public IEnumerable<ArticlePreviewModel> AllArticles { get; set; } = [];

    public ArticlePaginationModel Pagination { get; set; }
    public IEnumerable<ArticlePreviewModel>? HotArticles { get; set; }
}
