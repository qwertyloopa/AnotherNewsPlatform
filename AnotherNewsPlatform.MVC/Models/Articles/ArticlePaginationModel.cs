namespace AnotherNewsPlatform.MVC.Models.Articles
{
    public record ArticlePaginationModel
    {
        public int ActualPage { get; set; }
        public int PageCount { get; set; }
    }
}
