using AnotherNewsPlatform.App.Models;
using AnotherNewsPlatform.NewsService;

namespace AnotherNewsPlatform.App.Extensions
{
    public static class NewsDtoExtensions
    {
        /// <summary>
        /// Преобразует NewsDto в ArticleModel
        /// </summary>
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

        /// <summary>
        /// Преобразует коллекцию NewsDto в коллекцию ArticleModel
        /// </summary>
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