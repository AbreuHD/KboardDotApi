﻿using Core.Application.Interfaces.Repositories;
using Infraestructure.Persistence.Context;
using Infraestructure.Persistence.Repositories;
using KboardDotApi.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Infraestructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KboardDotContext>(options =>
                options.UseMySql(Environment.GetEnvironmentVariable("DefaultConnection") ?? configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(10, 6, 16)), m =>
                    m.MigrationsAssembly(typeof(KboardDotContext).Assembly.FullName).SchemaBehavior(MySqlSchemaBehavior.Ignore)));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICharacteristics_ProductRepository, Characteristics_ProductRepository>();
            services.AddTransient<ICharacteristicsRepository, CharacteristicsRepository>();
            services.AddTransient<IProduct_CategoryRepository, Product_CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISource_ProductRepository, Source_ProductRepository>();
            services.AddTransient<ISourceRepository, SourceRepository>();
            services.AddTransient<ITrackingTypeRepository, TrackingTypeRepository>();
            services.AddTransient<ITaxRepository, TaxRepository>();
            services.AddTransient<IProduct_TaxRepository, Product_TaxRepository>();
        }
    }
}
