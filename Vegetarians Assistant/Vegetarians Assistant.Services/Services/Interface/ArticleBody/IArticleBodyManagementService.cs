using Vegetarians_Assistant.Services.ModelView;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;

namespace Vegetarians_Assistant.Services.Services.Interface.IArticleBody
{
    public interface IArticleBodyManagementService
    {
        Task<List<ArticleBodyView?>> GetArticleBodyByArticleId(int id);
        Task<bool> CreateArticleBody(ArticleBodyView newArticleBody);
        Task<bool> UpdateArticleBodyByBodyId(int bodyId, ArticleBodyView updatedArticleBody);
        Task<bool> DeleteArticleBodyByBodyId(int id);
    }
}
