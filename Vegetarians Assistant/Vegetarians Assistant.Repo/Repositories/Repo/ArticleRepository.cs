using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Implement;

namespace Vegetarians_Assistant.Repo.Repositories.Repo
{
    public class ArticleRepository : GenericRepository<Article>
    {
        public ArticleRepository(VegetariansAssistantV3Context context) : base(context)
        {
        }

        public async Task<Article> GetByIDAsync(int id)
        {
            return await context.Articles.Include(a => a.Author).Include(a => a.ArticleLikes).FirstOrDefaultAsync(a => a.ArticleId == id);
        }

       
    }
}
