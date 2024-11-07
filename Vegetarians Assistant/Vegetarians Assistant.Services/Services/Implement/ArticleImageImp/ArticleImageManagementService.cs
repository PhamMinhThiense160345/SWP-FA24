using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IArticleImage;

namespace Vegetarians_Assistant.Services.Services.Implement.ArticleImageImp
{
    public class ArticleImageManagementService : IArticleImageManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleImageManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ArticleImageView?>> GetArticleImageByArticleId(int id)
        {

            try
            {
                var images = await _unitOfWork.ArticleImageRepository.FindAsync(c => c.ArticleId == id);
                var articleImageViews = new List<ArticleImageView>();

                foreach (var image in images)
                {
                    articleImageViews.Add(new ArticleImageView
                    {
                        ArticleImageId = image.ArticleImageId,
                        ArticleId = image.ArticleId,
                        ImageUrl = image.ImageUrl
                    });
                }
                return articleImageViews;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateArticleImage(ArticleImageView newArticleImage)
        {
            try
            {
                bool status = false;
                var image = _mapper.Map<ArticleImage>(newArticleImage);
                await _unitOfWork.ArticleImageRepository.InsertAsync(image);
                await _unitOfWork.SaveAsync();
                var insertedImage = await _unitOfWork.ArticleImageRepository.GetByIDAsync(image.ArticleImageId);

                if (insertedImage != null)
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                var insertedImage = (await _unitOfWork.ArticleImageRepository.FindAsync(a => a.ArticleImageId == newArticleImage.ArticleImageId)).FirstOrDefault();
                if (insertedImage != null)
                {
                    await _unitOfWork.ArticleImageRepository.DeleteAsync(insertedImage);
                    await _unitOfWork.SaveAsync();
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateArticleImageByArticleImageId(int articleImageId, string newImage)
        {
            try
            {
                var image = await _unitOfWork.ArticleImageRepository.GetByIDAsync(articleImageId);

                if (image == null)
                {
                    return false;
                }

                image.ImageUrl = newImage;
                await _unitOfWork.ArticleImageRepository.UpdateAsync(image);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteArticleImageByArticleImageId(int id)
        {
            try
            {
                var images = await _unitOfWork.ArticleImageRepository.GetByIDAsync(id);

                if (images != null)
                {

                    await _unitOfWork.ArticleImageRepository.DeleteAsync(images);
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
                throw;
            }
        }

    }
}
