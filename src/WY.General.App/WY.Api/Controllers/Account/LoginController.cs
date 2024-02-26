using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using WY.Application.Customers;
using WY.Application.Customers.Dtos;
using WY.Common.Token;
using WY.Entities.Account;

namespace WY.Api.Controllers.Account
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        private readonly ICustomerService customerService;
        private readonly IConfiguration configuration;

        public LoginController(ICustomerService customerService, IConfiguration configuration)
        {
            this.customerService = customerService;
            this.configuration = configuration;
        }
        /// <summary>
        /// 获取Token信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("token")]
        public async Task<IActionResult> TokenAsync(CustomerLoginInputDto model)
        {
            var entity = await customerService.GetCusotmerAsync(model.CustomerNo, model.Password);
            if (entity == null)
            {
                return BadRequest("Invalid Request");
            }

            var tokenOptions = configuration.GetSection("jwtToken").Get<JwtTokenOptions>();
            string token = TokenHelper.CreateToken(tokenOptions, entity.customerNo, entity.CustomerName);
            return Ok(token);
        }
    }
}
