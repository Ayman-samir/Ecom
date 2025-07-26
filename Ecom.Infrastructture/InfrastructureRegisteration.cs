using Ecom.core.Interfaces;
using Ecom.core.Services;
using Ecom.Infrastructture.Data;
using Ecom.Infrastructture.Repositories;
using Ecom.Infrastructture.Repositories.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ecom.Infrastructture
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection InfrastureConfiguration(this IServiceCollection services, IConfiguration config)
        {
            /*  services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));  
              services.AddScoped<ICategoryRepository,CategoryRepository>();
              services.AddScoped<IProductRepository, ProductRepository>();
              services.AddScoped<IPhotoRepository, PhotoRepository>();*/
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IImageMandmentService, ImageMangmentService>();
            var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            if (!Directory.Exists(wwwRootPath))
            {
                Directory.CreateDirectory(wwwRootPath);
            }

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(wwwRootPath)
            );
            //            services.AddSingleton<IFileProvider>(
            //    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            //);
            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(config.GetConnectionString("EcomDataBase"))
                 .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                 .EnableSensitiveDataLogging(true)
                // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking) to make globale no traking
) ;

            return services;
        }
    }
}
