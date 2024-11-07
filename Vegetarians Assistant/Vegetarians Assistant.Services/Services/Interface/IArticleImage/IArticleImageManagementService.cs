using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.IArticleImage
{
    public interface IArticleImageManagementService
    {
        Task<List<ArticleImageView?>> GetArticleImageByArticleId(int id);
        Task<bool> CreateArticleImage(ArticleImageView newArticleImage);
        Task<bool> UpdateArticleImageByArticleImageId(int articleImageId, string newImage);
        Task<bool> DeleteArticleImageByArticleImageId(int id);
    }
}
