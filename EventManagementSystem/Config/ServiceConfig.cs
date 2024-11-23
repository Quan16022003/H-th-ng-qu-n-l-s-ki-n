using Domain.Enum;
using Domain.Repositories;
using Persistence.Repositories;
using Services.Abtractions;
using Services;

namespace Web.Config
{
    public static class ServiceConfig
    {
        /// <summary>
        /// Register all repositories
        /// </summary>
        public static void RegisterAllRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<ICategoryEventRepository, CategoryEventRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IAttendeeRepository, AttendeeRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IOrderTicketRepository, OrderTicketRepository>();
        }

        /// <summary>
        /// Register all services
        /// </summary>
        public static void RegisterAllServices(this IServiceCollection services)
        {
            services.AddScoped<ISlugService, SlugService>();
            
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/dashboard/account/login"; // Trang đăng nhập cho admin
                options.AccessDeniedPath = "/dashboard/account/access-denied"; // Trang bị từ chối quyền
            });
        }
        
        public static void RegisterPolicy(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("StatisticsAccess",
                    policy => policy.RequireRole(Roles.Administrator, Roles.Organizer))
                .AddPolicy("EventManagement",
                    policy => policy.RequireRole(Roles.Administrator, Roles.Organizer))
                .AddPolicy("OrderManagement",
                    policy => policy.RequireRole(Roles.Administrator, Roles.Organizer))
                .AddPolicy("TicketManagement",
                    policy => policy.RequireRole(Roles.Administrator, Roles.Organizer))
                .AddPolicy("CategoryManagement",
                    policy => policy.RequireRole(Roles.Administrator))
                .AddPolicy("UserManagement",
                    policy => policy.RequireRole(Roles.Administrator))
                .AddPolicy("SiteManagement",
                    policy => policy.RequireRole(Roles.Administrator))
                .AddPolicy("SystemConfiguration",
                    policy => policy.RequireRole(Roles.Administrator));
        }
    }
}
