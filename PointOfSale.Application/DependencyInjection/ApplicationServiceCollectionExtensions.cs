using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PointOfSale.Application.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            // TODO Настроить валидаторы FluentValidation в контейнере зависимостей
            // TODO Добавить Поведение Pipeline для валидации FluentValidation и настроить этот Pipeline здесь

            return services;
        }
    }
}