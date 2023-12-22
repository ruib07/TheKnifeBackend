using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TheKnife.Services.Services;

namespace TheKnife.Services
{
    public static class DependencyRegister
    {
        public static void AddCustomServiceDependencyRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services
            .Scan(scan => scan.FromAssemblies(typeof(CommentsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(ContactsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(RegisterUsersService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(ReservationHistoryReservationsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(ReservationHistoryRestaurantsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(ReservationHistoryService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(ReservationsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(RestaurantResgistrationsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(RestaurantResponsiblesService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(RestaurantsService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services
            .Scan(scan => scan.FromAssemblies(typeof(UsersService).Assembly)
            .AddClasses(classe => classe.Where(p =>
                                               p.Name != null &&
                                               p.Name.EndsWith("Service") &&
                                               !p.IsInterface))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        }
    }
}
