using Autofac.Extensions.DependencyInjection;
using Autofac;
using WY.Application;

namespace WY.Api
{
    public static class AutofacExtensions
    {
        public static void AddAutofac(this WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<Autofac.ContainerBuilder>(builder =>
            {
                builder.RegisterModule<ApplicationModule>();
                builder.RegisterModule<ApiModule>();
            });
        }
    }
}
