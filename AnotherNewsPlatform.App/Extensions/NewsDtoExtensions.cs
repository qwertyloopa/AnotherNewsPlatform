using AnotherNewsPlatform.App.Models;
using AnotherNewsPlatform.NewsService;

namespace AnotherNewsPlatform.App.Extensions
{
    public static class NewsDtoExtensions
    {
        //for objects
        public static ArticleModel? ToArticleModel(this NewsDto? newsDto)
        {
            if (newsDto == null)
                return null;

            return new ArticleModel
            {
                Id = newsDto.Id,
                Title = newsDto.Title ?? string.Empty,
                Content = newsDto.Content ?? string.Empty,
                Text = newsDto.Text ?? string.Empty,
                PublishDate = newsDto.PublishDate,
                SourceId = newsDto.SourceId,
                SourceName = newsDto.SourceName ?? string.Empty,
                Comments = newsDto.Comments ?? new List<string>()
            };
        }

        // for lists
        public static List<ArticleModel> ToArticleModels(this IEnumerable<NewsDto> newsDtos)
        {
            if (newsDtos == null)
                return new List<ArticleModel>();

            return newsDtos
                .Select(dto => dto.ToArticleModel())
                .Where(article => article != null)
                .Select(article => article!)
                .ToList();
        }
    }
}