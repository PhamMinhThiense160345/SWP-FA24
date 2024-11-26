using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IArticleBody;

namespace Vegetarians_Assistant.Services.Services.Implement.ArticleBodyImp
{
    public class ArticleBodyManagementService : IArticleBodyManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleBodyManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ArticleBodyView?>> GetArticleBodyByArticleId(int id)
        {
            try
            {
                var bodies = await _unitOfWork.ArticleBodyRepository.FindAsync(ab => ab.ArticleId == id);
                var articleBodyViews = new List<ArticleBodyView>();

                foreach (var body in bodies)
                {
                    articleBodyViews.Add(new ArticleBodyView
                    {
                        BodyId = body.BodyId,
                        ArticleId = body.ArticleId,
                        Content = body.Content,
                        ImageUrl = body.ImageUrl,
                        Position = body.Position,
                        UserId = body.UserId
                    });
                }
                return articleBodyViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateArticleBody(ArticleBodyView newArticleBody)
        {
            try
            {
                bool status = false;
                var body = _mapper.Map<ArticleBody>(newArticleBody);
                await _unitOfWork.ArticleBodyRepository.InsertAsync(body);
                await _unitOfWork.SaveAsync();
                var insertedBody = await _unitOfWork.ArticleBodyRepository.GetByIDAsync(body.BodyId);

                if (insertedBody != null)
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                var insertedBody = (await _unitOfWork.ArticleBodyRepository.FindAsync(a => a.BodyId == newArticleBody.BodyId)).FirstOrDefault();
                if (insertedBody != null)
                {
                    await _unitOfWork.ArticleBodyRepository.DeleteAsync(insertedBody);
                    await _unitOfWork.SaveAsync();
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateArticleBodyByBodyId(int bodyId, ArticleBodyView updatedArticleBody)
        {
            try
            {
                var body = await _unitOfWork.ArticleBodyRepository.GetByIDAsync(bodyId);

                if (body == null)
                {
                    return false;
                }

                body.Content = updatedArticleBody.Content;
                body.ImageUrl = updatedArticleBody.ImageUrl;
                body.Position = updatedArticleBody.Position;

                await _unitOfWork.ArticleBodyRepository.UpdateAsync(body);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteArticleBodyByBodyId(int id)
        {
            try
            {
                var body = await _unitOfWork.ArticleBodyRepository.GetByIDAsync(id);

                if (body != null)
                {
                    await _unitOfWork.ArticleBodyRepository.DeleteAsync(body);
                    await _unitOfWork.SaveAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
