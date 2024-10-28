using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;
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
            _unitOfWork.CommentRepository.UpdateAsync(comment);
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
    }
}
