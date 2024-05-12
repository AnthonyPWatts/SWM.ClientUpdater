using Microsoft.Extensions.DependencyInjection;
using SWM.ClientUpdater.Application.Interfaces;
using SWM.ClientUpdater.Application.Services;

namespace SWM.ClientUpdater.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddClientUpdaterApplication(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        return services;
    }
}
