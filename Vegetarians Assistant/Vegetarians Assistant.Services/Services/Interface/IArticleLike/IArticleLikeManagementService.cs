using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.IArticleLike
{
    public interface IArticleLikeManagementService
    {
        Task<bool> CreateArticleLike(ArticleLikeView newArticleLike);
        Task<List<ArticleLikeView?>> GetArticleLikeByArticleId(int id);
        Task<bool> DeleteArticleLikeByUserId(ArticleLikeView deleteArticleLike);
    }
}
