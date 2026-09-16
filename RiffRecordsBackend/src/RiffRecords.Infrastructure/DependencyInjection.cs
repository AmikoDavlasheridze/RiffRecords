using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiffRecords.Application.Abstractions;
using RiffRecords.Infrastructure.Persistence;
using RiffRecords.Infrastructure.Repositories;
using RiffRecords.Infrastructure.Security;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RiffRecords.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this  IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IVinylRepository, VinylRepository>();
            services.AddScoped<IBandRepository, BandRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWishlistedItemRepository, WishlistedItemRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHash, PasswordHasher>();

            return services;
        }
    }
}
