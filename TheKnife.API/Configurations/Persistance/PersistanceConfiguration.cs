using Microsoft.EntityFrameworkCore;
using TheKnife.EntityFramework;

namespace TheKnife.API.Configurations.Persistance
{
    public static class PersistanceConfiguration
    {
        public static void AddCustomDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TheKnifeDb");

            services.AddDbContext<TheKnifeDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptions =>
                {
                    var assembly = typeof(TheKnifeDbContext).Assembly;
                    var assemblyName = assembly.GetName();

                    sqlServerOptions.MigrationsAssembly(assemblyName.Name);
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 2,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            });
        }
    }
}
