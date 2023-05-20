using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;

namespace Persistence
{
    namespace Persistence
    {
        public static class DependencyInjection
        {
            public static void AddPersistence(this IServiceCollection services, IConfiguration config)
            {
                services.AddDbContext<StorageDbContext>(options =>
                {
                    options.UseSqlServer(
                        config.GetConnectionString("StorageDb"),
                        m => m.MigrationsAssembly(typeof(StorageDbContext).Assembly.FullName));
                });
                services.AddScoped<IStorageDbContext>(provider => provider.GetService<StorageDbContext>());
            }
        }
    }

}
