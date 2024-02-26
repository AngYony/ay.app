using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Runtime.Loader;
using WY.Application.Articles;
using WY.Common.IocTags;

namespace WY.Api
{
    public class ApiModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 获取所有创建的项目Lib
            var libs = DependencyContext.Default
            .CompileLibraries
            .Where(x => !x.Serviceable && x.Type == "project").ToList();
            // 将lib转成Assembly
            List<Assembly> assemblies = new();
            foreach (var lib in libs)
            {
                assemblies.Add(AssemblyLoadContext.Default
                .LoadFromAssemblyName(new AssemblyName(lib.Name)));
            }
            builder.RegisterAssemblyTypes(assemblies.ToArray())
               .Where(t => t.IsAssignableTo<IIocSingletonTag>() && !t.IsAbstract)
               .AsSelf().AsImplementedInterfaces()
               .SingleInstance(); //启用属性注入：.PropertiesAutowired();

            //注入Scope
            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(t => t.IsAssignableTo<IIocScopeTag>() && !t.IsAbstract)
                .AsSelf().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //注入Transient
            builder.RegisterAssemblyTypes(assemblies.ToArray())
                .Where(t => t.IsAssignableTo<IIocTransientTag>() && !t.IsAbstract)
                .AsSelf().AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies.ToArray())
               .Where(t => t.IsAssignableTo<ControllerBase>() && !t.IsAbstract)
               .AsSelf();
               //.PropertiesAutowired();
        }
    }
}
