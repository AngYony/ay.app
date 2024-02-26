using Autofac.Core;
using Microsoft.OpenApi.Models;

namespace WY.Api
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "WY.Api", Version = "v1" });

                // 添加安全定义
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "格式：Bearer {token}",
                    Name = "Authorization", // 默认的参数名
                    In = ParameterLocation.Header,// 放于请求头中
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                // 添加安全要求
                option.AddSecurityRequirement(new OpenApiSecurityRequirement{
                   {
                        new OpenApiSecurityScheme{
                             Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, new string[]{}
                   }
                });
            });
            return services;

        }

        public static void UseCusSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            // https://www.whuanle.cn/archives/21254
            //var descriptionProvider = app.Services.GetRequiredService<IApiDescriptionGroupCollectionProvider>();
            //app.UseSwaggerUI(options =>
            //{
            //    // 默认的
            //    options.SwaggerEndpoint($"v1/swagger.json", "v1");
            //    foreach (var description in descriptionProvider.ApiDescriptionGroups.Items)
            //    {
            //        if (description.GroupName == null) continue;
            //        options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName);
            //    }
            //});
        }
    }
}
