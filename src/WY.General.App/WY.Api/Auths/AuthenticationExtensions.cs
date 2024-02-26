using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WY.Common.Token;

namespace WY.Api
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,IConfiguration configuration)
        {
            var token= configuration.GetSection("Jwt").Get<JwtTokenModel>();
            #region Jwt验证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                opt =>
                {
                    // 是否是Https，默认true
                    opt.RequireHttpsMetadata = false;
                    opt.SaveToken = true;
                    opt.TokenValidationParameters = new()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Security)),
                        ValidIssuer = token.Issuer,
                        ValidAudience = token.Audience
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
