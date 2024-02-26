using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WY.Common.Token;

namespace WY.Api
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var token = configuration.GetSection("jwtToken").Get<JwtTokenOptions>();

            #region Jwt验证
            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // 注册JWT身份验证架构
            .AddJwtBearer(
                opt =>
                {
                    // 是否是Https，默认true
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        // 这里就是关键，签名证书、颁发者名称等和颁发服务一致才能正确验证
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                        ValidIssuer = token.Issuer,
                        ValidAudience = token.Audience,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            //此处终止代码
                            context.HandleResponse();
                            var res = "{\"code\":401,\"err\":\"无权限\"}";
                            context.Response.ContentType = "application/json";
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.WriteAsync(res);
                            return Task.FromResult(0);
                        }
                    };
                }
            );
            #endregion

            return services;
        }
    }
}
