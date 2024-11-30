using Web.Utils.ViewsPathServices.Implementations;
using Web.Utils.ViewsPathServices;

namespace Web.Config
{
    public static class PathProviderConfig
    {
        public static void RegisterPathProvideManager(this IServiceCollection services)
        {
            // key: area name, value: service match
            RegisterViewsPathProvider(services);
            RegisterComponentsPathProvider(services);

            services.AddScoped<IPathProvideManager, PathProvideManager>();
        }

        private static void RegisterViewsPathProvider(IServiceCollection services)
        {
            services.AddKeyedScoped<IPathProvider, DefaultPathProvider>("default");
            services.AddKeyedScoped<IPathProvider, DashboardPathProvider>("dashboard");
        }

        private static void RegisterComponentsPathProvider(IServiceCollection services)
        {
            services.AddKeyedScoped<IPathProvider, DashboardComponentPathProvider>("dashboard_component");
        }
    }
}
