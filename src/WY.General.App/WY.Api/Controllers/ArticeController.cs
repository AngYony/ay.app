﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WY.Application.Articles;
using WY.Entities.BBS;

namespace WY.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticeController : ControllerBase
    {
        private readonly IArticleService articleService;

        public ArticeController(IArticleService articleService)
        {
            this.articleService = articleService;
        }

        [HttpGet("wy")]
        public async Task<List<Article>> GetWy()
        {
            return await articleService.GetWy();
        }
        [AcceptVerbs("Get","Post",Route ="test")]
        public IActionResult abcc()
        {
            return Content("同时支持Get和Post请求");
        }
        
    }
}