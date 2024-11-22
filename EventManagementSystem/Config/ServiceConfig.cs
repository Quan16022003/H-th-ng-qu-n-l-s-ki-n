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
        }
    }
}
