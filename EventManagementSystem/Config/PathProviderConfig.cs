﻿using Web.Utils.ViewsPathServices.Implementations;
using Web.Utils.ViewsPathServices;

namespace Web.Config
{
    public static class PathProviderConfig
    {
        public static void RegisterPathProvideManager(this IServiceCollection services)
        {
            // key: area name, value: service match
            services.AddKeyedScoped<IPathProvider, DashboardPathProvider>("Dashboard");
            services.AddKeyedScoped<IPathProvider, ProfilePathProvider>("Profile");

            services.AddScoped<IPathProvideManager, PathProvideManager>();
        }
    }
}