﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WY.Model.Models;

namespace WY.Services.Articles
{
    public interface IArticleService
    {
        Task<List<Article>> GetWy();
    }
}