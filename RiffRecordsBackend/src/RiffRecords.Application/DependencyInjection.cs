using Microsoft.Extensions.DependencyInjection;
using RiffRecords.Application.Abstractions;
using RiffRecords.Application.Services;
using RiffRecords.Application.Abstractions.serviceAbstractions;

namespace RiffRecords.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IVinylService, VinylService>();
            services.AddScoped<IBandService, BandService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWishlistedItemService, WishlistedItemService>();

            return services;
        }
    }
}