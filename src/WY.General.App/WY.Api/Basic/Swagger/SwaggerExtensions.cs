using Autofac.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace WY.Api
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            //services.AddSwaggerGen();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WY.Api",
                    Description = "轻量级 Web Api 项目框架",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "AngYony",
                        Email = "angyony@163.com",
                        Url = new Uri("https://github.com/angyony/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache License 2.0"
                    }

                });

                // 添加安全定义
                option.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Description = "格式：Bearer {token}",
                        Name = "Authorization", // 默认的参数名
                        In = ParameterLocation.Header,// 放于请求头中
                        Type = SecuritySchemeType.ApiKey,
                        BearerFormat = "JWT",
                        Scheme = JwtBearerDefaults.AuthenticationScheme
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

                //启用XML注释
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                option.IncludeXmlComments(xmlPath);
            });
            return services;

        }

        public static void UseCusSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WY.Api v1");

            });



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
