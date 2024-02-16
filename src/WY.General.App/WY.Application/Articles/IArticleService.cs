using WY.Entities.BBS;

namespace WY.Application.Articles
{
    public interface IArticleService
    {
        Task<List<Article>> GetWy();
    }
}