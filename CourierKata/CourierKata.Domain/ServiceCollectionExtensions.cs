using CourierKata.Domain.Implementation;
using CourierKata.Primary.Ports.OperationContracts;
using Microsoft.Extensions.DependencyInjection;

namespace CourierKata.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomain(this IServiceCollection services)
            => services.AddScoped<IOrdersService, OrdersService>();
    }
}