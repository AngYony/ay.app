﻿using Microsoft.EntityFrameworkCore;
using WY.Common.IocTags;
using WY.Entities.BBS;
using WY.EntityFramework.Repositories;

namespace WY.Application.Articles
{
    public class ArticleServices : IArticleService,IIocTransientTag
    {
        private readonly IRepository<Article,int> repository;

        public ArticleServices(IRepository<Article,int> repository)
        {
            this.repository = repository;
        }

        public async Task<List<Article>> GetWy()
        {
            return await repository.GetAll().Where(a => a.Id > 0).AsNoTracking().ToListAsync();
        }

        public string GetTitle(string title)
        {
            return title;
        }

    }
}