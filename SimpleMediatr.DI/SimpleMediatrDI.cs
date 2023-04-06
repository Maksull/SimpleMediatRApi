using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleMediatr.Data.Database;
using SimpleMediatr.Data.Repository;
using SimpleMediatr.Data.Repository.Interfaces;
using SimpleMediatr.Data.UnitOfWorks;
using SimpleMediatr.FluentValidation.Products;
using SimpleMediatr.Mapster;
using SimpleMediatr.MediatR.Behaviors;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Services;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.DI
{
    public static class SimpleMediatrDI
    {
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
            FluentValidationRegister(services);
            MapsterRegister(services);
            MediatRRegister(services);
            DataRegister(services, configuration);
            ServicesRegister(services);

        }

        private static void FluentValidationRegister(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateProductDtoValidator).Assembly);

            services.AddFluentValidationAutoValidation();
        }

        private static void MapsterRegister(IServiceCollection services)
        {
            TypeAdapterConfig config = new();
            config.Apply(new MapsterRegister());
            services.AddSingleton(config);

            services.AddSingleton<IMapper>(sp =>
            {
                return new ServiceMapper(sp, config);
            });
        }

        private static void MediatRRegister(IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetProductsQuery>());

            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        private static void DataRegister(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SimpleMediatrDataContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString("SimpleMediatrDb"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductRepository, ProductRepository>()
                .AddScoped(provider => new Lazy<IProductRepository>(() => provider.GetRequiredService<IProductRepository>()));
            services.AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped(provider => new Lazy<ICategoryRepository>(() => provider.GetRequiredService<ICategoryRepository>()));
        }

        private static void ServicesRegister(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
        }

    }
}
