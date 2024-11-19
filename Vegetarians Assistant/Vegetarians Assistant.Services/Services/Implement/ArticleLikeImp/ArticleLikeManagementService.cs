using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IArticleLike;

namespace Vegetarians_Assistant.Services.Services.Implement.ArticleLikeImp
{
    public class ArticleLikeManagementService : IArticleLikeManagementService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ArticleLikeManagementService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ArticleLikeView?>> GetArticleLikeByArticleId(int id)
        {

            try
            {
                var articleLikes = await _unitOfWork.ArticleLikeRepository.FindAsync(c => c.ArticleId == id);
                var articleLikeViews = new List<ArticleLikeView>();

                foreach (var articleLike in articleLikes)
                {
                    articleLikeViews.Add(new ArticleLikeView
                    {
                        ArticleId = articleLike.ArticleId,
                        UserId = articleLike.UserId,
                        LikeId = articleLike.LikeId,
                        LikeDate = articleLike.LikeDate
                    });
                }
                return articleLikeViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateArticleLike(ArticleLikeView newArticleLike)
        {
            try
            {
                bool status = false;

                var articleLike = _mapper.Map<ArticleLike>(newArticleLike);

                var exitsArticle = (await _unitOfWork.ArticleLikeRepository.FindAsync(a => a.ArticleId == newArticleLike.ArticleId && a.UserId == newArticleLike.UserId)).FirstOrDefault();

                if (exitsArticle == null)
                {
                    await _unitOfWork.ArticleLikeRepository.InsertAsync(articleLike);
                    await _unitOfWork.SaveAsync();
                    status = true;
                }
                else
                {
                    await _unitOfWork.ArticleLikeRepository.DeleteAsync(exitsArticle);
                    await _unitOfWork.SaveAsync();
                }
                return status;
            }
            catch (Exception ex)
            {
                var exitsArticle = (await _unitOfWork.ArticleLikeRepository.FindAsync(a => a.LikeId == newArticleLike.LikeId)).FirstOrDefault();
                if (exitsArticle != null)
                {
                    await _unitOfWork.ArticleLikeRepository.DeleteAsync(exitsArticle);
                    await _unitOfWork.SaveAsync();
                }

                throw new Exception($"Lỗi khi like: {ex.Message}");
            }
        }

        public async Task<bool> DeleteArticleLikeByUserId(ArticleLikeView deleteArticleLike)
        {
            try
            {
                bool status = false;
                var like = _mapper.Map<ArticleLike>(deleteArticleLike);
                var existLike = (await _unitOfWork.ArticleLikeRepository.FindAsync(a => a.ArticleId == deleteArticleLike.ArticleId && a.UserId == deleteArticleLike.UserId)).FirstOrDefault();
                if (existLike != null)
                {
                    await _unitOfWork.ArticleLikeRepository.DeleteAsync(existLike);
                    await _unitOfWork.SaveAsync();
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
