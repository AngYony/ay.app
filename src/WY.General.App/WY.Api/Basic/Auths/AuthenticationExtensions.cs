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
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                        RequireExpirationTime = true,
                    };
                    opt.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            //配置同时支持从url中获取Token
                            context.Token = context.Request.Query["access_token"];
                            return Task.CompletedTask;
                        },
                        //未授权时调用
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
