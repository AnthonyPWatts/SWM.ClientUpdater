using Microsoft.Extensions.DependencyInjection;
using SWM.ClientUpdater.Application.Interfaces;

namespace SWM.ClientUpdater.ExternalServices;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalServices(this IServiceCollection services, string externalServiceEndpoint)
    {
        services.AddHttpClient();
        services.AddScoped<IOnboardingExternalService>(provider =>
        {
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            return new OnboardingExternalService(httpClientFactory, externalServiceEndpoint);
        });

        return services;
    }
}
