using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using System.Security.Policy;
using Web.Utils.UrlTransform;

namespace Web.Config
{
    public static class RouteConfig
    {
        /// <summary>
        /// Register slugify transformer
        /// </summary>
        public static void RegisterSlugifyTransformer(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });
        }

        /// <summary>
        /// Register all area route that exist in app
        /// </summary>
        public static void RegisterAllRoutes(this WebApplication app)
        {
            app.MapAreaControllerRoute(
                name: "Dashboard",
                areaName: "Dashboard",
                pattern: "dashboard/{controller:slugify}/{action:slugify}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");
        }   
    }
}