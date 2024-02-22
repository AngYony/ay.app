using Autofac;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using WY.Application.Articles;
using WY.EntityFramework.Repositories;

namespace WY.Application
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 获取所有创建的项目Lib
            //var libs = DependencyContext.Default
            //.CompileLibraries
            //.Where(x => !x.Serviceable && x.Type == "project").ToList();
            //// 将lib转成Assembly
            //List<Assembly> assemblies = new();
            //foreach (var lib in libs)
            //{
            //    assemblies.Add(AssemblyLoadContext.Default
            //    .LoadFromAssemblyName(new AssemblyName(lib.Name)));
            //}
            //builder.RegisterAssemblyTypes(assemblies.ToArray())
            //    .Where(t =>  t.IsAssignableTo<IocTagScope>() && !t.IsAbstract)
            //    .AsSelf().AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();
            //builder.RegisterType<ArticleServices>().As<IArticleService>();
        }
    }
}
