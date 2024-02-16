using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.EntityFramework.Repositories;
using WY.Model.Models;

namespace WY.Services.Articles
{
    public class ArticleServices : IArticleService
    {
        private readonly IRepository<Article> repository;

        public ArticleServices(IRepository<Article> repository)
        {
            this.repository = repository;
        }

        public async Task<List<Article>> GetWy()
        {
            return await repository.GetAll().Where(a => a.Id > 0).AsNoTracking().ToListAsync();
        }
    }
}
