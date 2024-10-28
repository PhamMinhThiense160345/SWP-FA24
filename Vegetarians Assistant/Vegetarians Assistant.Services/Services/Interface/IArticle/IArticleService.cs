using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.Services.Services.Interface.IArticle
{
    public interface IArticleService
    {
        Task<ArticleView> GetById(int id);

        Task<List<CommentView>> getArticleComment(int id);

    }

}

