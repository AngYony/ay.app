using WY.Common.IocTags;
using WY.Entities.BBS;

namespace WY.Application.Articles
{
    public interface IArticleService
    {
        Task<List<Article>> GetWy();

        string GetTitle(string title);
    }
}