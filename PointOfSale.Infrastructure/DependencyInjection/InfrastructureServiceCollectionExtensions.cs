using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Infrastructure.Persistence.DbContexts;

namespace PointOfSale.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContexts(services, configuration);
            AddMessageBus(services);

            return services;
        }

        private static void AddDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IPointOfSaleContext, PointOfSaleContext>(
                builder => builder.UseSqlServer(configuration.GetConnectionString(nameof(PointOfSaleContext))));
        }

        private static void AddMessageBus(IServiceCollection services)
        {
            services.AddMassTransit(configurator =>
            {
                configurator.UsingInMemory((context, configurator) => configurator.ConfigureEndpoints(context));

                configurator.AddRider(configurator =>
                {
                    configurator.UsingEventHub((context, configurator) =>
                    {
                        configurator.Host("Endpoint=sb://pointofsale.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=1VR7ARd/GJ+IHkKTXhDGQ6koEzYHP951TUK8L89F4mg=");
                    });
                });

                services.AddMassTransitHostedService();
            });
        }
    }
}