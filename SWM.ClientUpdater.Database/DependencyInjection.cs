using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SWM.ClientUpdater.Application.Interfaces;

namespace SWM.ClientUpdater.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddClientDb(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ClientDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IClientDbContext, ClientDbContext>();
        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}
