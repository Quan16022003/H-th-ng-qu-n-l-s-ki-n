using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.Abtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderCancelledService : BackgroundService
    {
        private readonly ILogger<OrderCancelledService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _cancellationCheckInterval; // How often to check for cancellations

        public OrderCancelledService(ILogger<OrderCancelledService> logger,
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _cancellationCheckInterval = TimeSpan.FromMinutes(configuration.GetValue<int>("OrderCancellationCheckInterval", 5)); // Default to 5 minutes
            _scopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessOrderCancellationsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing order cancellations.");
                }
                await Task.Delay(_cancellationCheckInterval, stoppingToken);
            }
        }

        private async Task ProcessOrderCancellationsAsync(CancellationToken stoppingToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                var ordersToCancel = await _orderRepository.GetOrdersReadyForCancellationAsync(stoppingToken);

                foreach (var order in ordersToCancel)
                {
                    try
                    {
                        await CancelOrderAsync(order, stoppingToken, (IServiceProvider)scope);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error cancelling order {order.Id}");
                    }
                }
            }
        }

        private async Task CancelOrderAsync(Orders order, CancellationToken stoppingToken, IServiceProvider scope) // Added scope parameter
        {
            using (var subScope = scope.CreateScope()) // Using a sub-scope (optional but good practice)
            {
                var _serviceManager = subScope.ServiceProvider.GetRequiredService<IServiceManager>();
                await _serviceManager.OrdersService.CancelledOrderAsync(order.Id);
                _logger.LogInformation($"Order {order.Id} cancelled automatically.");
            }
        }
    }
}
