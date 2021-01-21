using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PointOfSale.Application.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IMvcBuilder AddValidation(this IMvcBuilder services)
        {
            services.AddFluentValidation(
                configuration => configuration.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}