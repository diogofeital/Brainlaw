using DesafioBrainlaw.Application.Interfaces;
using DesafioBrainlaw.Application.Services;
using DesafioBrainlaw.Domain.Interfaces.Repositories;
using DesafioBrainlaw.Domain.Interfaces.UnitOfWork;
using DesafioBrainlaw.Domain.Shared.Interface.Notification;
using DesafioBrainlaw.Domain.Shared.Notifications;
using DesafioBrainlaw.Infrastructure.Context;
using DesafioBrainlaw.Infrastructure.Repositories;
using DesafioBrainlaw.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioBrainlaw.Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DesafioBrainlawContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotifier, Notifier>();

            ProductIocRegister(services);

            return services;
        }

        private static void ProductIocRegister(IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}