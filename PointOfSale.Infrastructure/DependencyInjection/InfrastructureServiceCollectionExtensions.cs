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

            return services;
        }

        private static void AddDbContexts(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IPointOfSaleContext, PointOfSaleContext>(
                builder => builder.UseSqlServer(configuration.GetConnectionString(nameof(PointOfSaleContext))));
        }
    }
}