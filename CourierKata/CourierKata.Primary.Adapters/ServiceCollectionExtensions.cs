using CourierKata.Domain;
using CourierKata.Primary.Adapters.Implementation;
using CourierKata.Primary.Ports.OperationContracts;
using Microsoft.Extensions.DependencyInjection;

namespace CourierKata.Primary.Adapters
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPrimary(this IServiceCollection services)
        {
            services.AddScoped<IOrdersAdapter, OrdersAdapter>();
            services.AddDomain();
        }
    }
}