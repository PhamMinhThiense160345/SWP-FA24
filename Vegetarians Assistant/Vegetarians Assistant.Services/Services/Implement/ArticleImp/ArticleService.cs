using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;
using Vegetarians_Assistant.Services.Enum;
using Vegetarians_Assistant.Services.ModelView;
using Vegetarians_Assistant.Services.Services.Interface.IArticle;

namespace Vegetarians_Assistant.Services.Services.Interface.ArticleImp
{
    public class ArticleService : IArticleService
    {
        private readonly ArticleRepository _articleRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ArticleService(ArticleRepository articleRepo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _articleRepo = articleRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ArticleView?> Edit(ArticleView? view)
        {
            if (view != null)
            {
                var article = _mapper.Map<Article>(view);
                await _articleRepo.UpdateAsync(article);
                return view;
            }
            return null;
        }

        public async Task<List<CommentView>> getArticleComment(int id)
        {
            var comments = await _articleRepo.getArticleComment(id);
            var commentViews = new List<CommentView>();
            foreach (var comment in comments)
            {
                commentViews.Add(MapToCommentView(comment));
            }

            return commentViews;
        }

        public async Task<CommentView> postComment(CommentView view)
        {
            var comment = _mapper.Map<Comment>(view);
            await _unitOfWork.CommentRepository.InsertAsync(comment);
            await _unitOfWork.SaveAsync();
            return view;
        }

        public async Task<ArticleView?> GetById(int id)
        {
            var article = await _articleRepo.GetByIDAsync(id);

            if (article != null)
            {
                return MapToArticleView(article);
            }

            return null;
        }

        public ArticleView MapToArticleView(Article article)
        {
            var articleView = _mapper.Map<ArticleView>(article);

            articleView.AuthorName = article.Author.Username;

            articleView.Likes = article.ArticleLikes?.Count();

            articleView.ArticleImages = new List<string>();

            foreach (var image in article.ArticleImages)
            {
                articleView.ArticleImages.Add(image.ImageUrl);
            }

            return articleView;
        }

        public CommentView MapToCommentView(Comment comment)
        {
            var commentView = _mapper.Map<CommentView>(comment);

            commentView.UserName = comment.User.Username;

            return commentView;
        }
        public async Task<List<ArticleView?>> GetArticleByRoleId(int id)
        {
            try
            {
                var dish = await _unitOfWork.UserRepository.GetAsync(c => c.RoleId == id);
                var userIds = dish.Select(u => u.UserId).ToList();
                if (id.Equals(3) || id.Equals(5))
                {
                    var articles = await _unitOfWork.ArticleRepository.GetAsync(a => a.AuthorId.HasValue && userIds.Contains(a.AuthorId.Value));
                    var articleViews = new List<ArticleView>();

                    var authorIds = new HashSet<int>();
                    foreach (var article in articles)
                    {
                        if (article.AuthorId.HasValue)
                        {
                            authorIds.Add(article.AuthorId.Value);
                        }
                    }

                    var users = await _unitOfWork.UserRepository.GetAsync(dp => authorIds.Contains(dp.UserId));

                    var preferenceDictionary = new Dictionary<int, string>();
                    foreach (var preference in users)
                    {
                        preferenceDictionary[preference.UserId] = preference.Username;
                    }

                    foreach (var article in articles)
                    {
                        articleViews.Add(new ArticleView
                        {
                            ArticleId = article.ArticleId,
                            Title = article.Title,
                            Content = article.Content,
                            Status = article.Status,
                            ArticleImages = article.ArticleImages.Select(img => img.ImageUrl).ToList(),
                            Likes = article.ArticleLikes.Count(),
                            AuthorId = article.AuthorId,
                            ModerateDate = article.ModerateDate,
                            AuthorName = article.AuthorId.HasValue && preferenceDictionary.ContainsKey(article.AuthorId.Value)
                        ? preferenceDictionary[article.AuthorId.Value]
                        : null
                        });
                    }
                    return articleViews.OrderByDescending(article => article.ModerateDate).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateArticleStatusByArticleId(int articleId, string newStatus)
        {
            try
            {
                var article = await _unitOfWork.ArticleRepository.GetByIDAsync(articleId);

                if (article == null)
                {
                    return false;
                }

                article.Status = newStatus;
                article.ModerateDate = DateOnly.FromDateTime(DateTime.Now);
                await _unitOfWork.ArticleRepository.UpdateAsync(article);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> CreateArticleByCustomer(ArticleView newArticle)
        {
            try
            {
                bool status = false;

                newArticle.Status = "pending";
                newArticle.ModerateDate = DateOnly.FromDateTime(DateTime.Now);
                var article = _mapper.Map<Article>(newArticle);

                await _unitOfWork.ArticleRepository.InsertAsync(article);
                await _unitOfWork.SaveAsync();

                var insertedArticle = await _unitOfWork.ArticleRepository.GetByIDAsync(article.ArticleId);

                if (insertedArticle != null)
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                var insertedArticle = (await _unitOfWork.ArticleRepository.FindAsync(a => a.Title == newArticle.Title && a.AuthorId == newArticle.AuthorId)).FirstOrDefault();
                if (insertedArticle != null)
                {
                    await _unitOfWork.ArticleRepository.DeleteAsync(insertedArticle);
                    await _unitOfWork.SaveAsync();
                }

                throw new Exception($"Lỗi khi tạo bài viết: {ex.Message}");
            }
        }

        public async Task<bool> CreateArticleByNutritionist(ArticleView newArticle)
        {
            try
            {
                bool status = false;

                newArticle.Status = "accepted";

                var article = _mapper.Map<Article>(newArticle);

                await _unitOfWork.ArticleRepository.InsertAsync(article);
                await _unitOfWork.SaveAsync();

                var insertedArticle = await _unitOfWork.ArticleRepository.GetByIDAsync(article.ArticleId);

                if (insertedArticle != null)
                {
                    status = true;
                }

                return status;
            }
            catch (Exception ex)
            {
                var insertedArticle = (await _unitOfWork.ArticleRepository.FindAsync(a => a.Title == newArticle.Title && a.AuthorId == newArticle.AuthorId)).FirstOrDefault();
                if (insertedArticle != null)
                {
                    await _unitOfWork.ArticleRepository.DeleteAsync(insertedArticle);
                    await _unitOfWork.SaveAsync();
                }

                throw new Exception($"Lỗi khi tạo bài viết: {ex.Message}");
            }
        }

        public async Task<List<ArticleView?>> GetArticleByAuthorId(int id)
        {
            try
            {
                // Nạp trước ArticleImages và ArticleLikes bằng Include
                var articles = await _unitOfWork.ArticleRepository.FindAsync(c => c.AuthorId == id, a => a.ArticleImages, a => a.ArticleLikes);

                var articleViews = new List<ArticleView>();

                // Lấy danh sách các UserId từ bài viết để map với AuthorName
                var authorIds = new HashSet<int>();
                foreach (var article in articles)
                {
                    if (article.AuthorId.HasValue)
                    {
                        authorIds.Add(article.AuthorId.Value);
                    }
                }

                var users = await _unitOfWork.UserRepository.GetAsync(dp => authorIds.Contains(dp.UserId));

                var preferenceDictionary = new Dictionary<int, string>();
                foreach (var preference in users)
                {
                    preferenceDictionary[preference.UserId] = preference.Username;
                }

                // Tạo danh sách ArticleView
                foreach (var article in articles)
                {
                    articleViews.Add(new ArticleView
                    {
                        ArticleId = article.ArticleId,
                        Title = article.Title,
                        Content = article.Content,
                        Status = article.Status,
                        ArticleImages = article.ArticleImages.Select(img => img.ImageUrl).ToList(),
                        Likes = article.ArticleLikes.Count(), // Đếm số lượng Likes
                        AuthorId = article.AuthorId,
                        ModerateDate = article.ModerateDate,
                        AuthorName = article.AuthorId.HasValue && preferenceDictionary.ContainsKey(article.AuthorId.Value)
                            ? preferenceDictionary[article.AuthorId.Value]
                            : null
                    });
                }

                return articleViews.OrderByDescending(article => article.ModerateDate).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCommentByUserId(CommentView deleteComment)
        {
            try
            {
                bool status = false;
                var comment = _mapper.Map<Comment>(deleteComment);
                var existComment = (await _unitOfWork.CommentRepository.FindAsync(a => a.ArticleId == deleteComment.ArticleId && a.UserId == deleteComment.UserId)).FirstOrDefault();
                if (existComment != null)
                {
                    await _unitOfWork.CommentRepository.DeleteAsync(existComment);
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

        public async Task<bool> UpdateArticleByArticleId(ArticleInfoView updateArticle)
        {
            try
            {
                bool status = false;
                var article = _mapper.Map<Article>(updateArticle);
                article.ModerateDate = DateOnly.FromDateTime(DateTime.Now);
                await _unitOfWork.ArticleRepository.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                var exsitArticle = (await _unitOfWork.ArticleRepository.FindAsync(c => c.ArticleId == updateArticle.ArticleId)).FirstOrDefault();

                if (exsitArticle != null)
                {
                    var favo = new Article
                    {
                        ArticleId = exsitArticle.ArticleId,
                        Title = exsitArticle.Title,
                        Content = exsitArticle.Content,
                        Status = exsitArticle.Status,
                        AuthorId = exsitArticle.AuthorId,
                        ModerateDate = exsitArticle?.ModerateDate,
                    };
                    await _unitOfWork.SaveAsync();
                    status = true;
                }
                return status;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
