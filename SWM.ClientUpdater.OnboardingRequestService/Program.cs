using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using SWM.ClientUpdater.Application;
using SWM.ClientUpdater.Application.Interfaces;
using SWM.ClientUpdater.Database;
using SWM.ClientUpdater.ExternalServices;

var services = GetConfiguredServiceCollection();
var provider = services.BuildServiceProvider();

await ProcessClientsAsync(
    provider.GetRequiredService<IClientService>(), 
    provider.GetRequiredService<ILogger<Program>>());

static ServiceCollection GetConfiguredServiceCollection()
{
    var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.development.json", optional: true)
    .Build();

    var clientDbConnectionString = configuration.GetConnectionString("ClientDb")
        ?? throw new Exception("ClientDb connection string is required");

    var clientOnboardingApiEndpoint = configuration.GetSection("ApiEndpoints").GetSection("ClientOnboardingApi").Value
        ?? throw new Exception("ClientOnboardingApi endpoint is required");

    var services = new ServiceCollection();
    services.AddClientDb(clientDbConnectionString);
    services.AddClientUpdaterApplication();
    services.AddExternalServices(clientOnboardingApiEndpoint);

    return services;
}

static async Task ProcessClientsAsync(IClientService clientService, ILogger log)
{
    var clients = await clientService.GetOnboardOutstandingClientsAsync(joinedAtLeastDaysAgo: 7);
    if (clients == null || !clients.Any())
    {
        log.LogInformation("No clients to onboard.");
        return;
    }

    var externalServiceResult = await clientService.RequestOnboardingAsync(clients);
    if (externalServiceResult.IsSuccess == false)
    {
        log.LogError("Onboarding request failed. {ErrorMessage}", externalServiceResult.Message);
        return;        
    }

    // Could/should change onboarding status of clients in db to 'requested' if we get here.
    // Not implemented in this example - very sunny outside
    // e.g.
    //await clientService.MarkClientsOnboardingRequestedAsync(clients.Select(c => c.ID).ToList());

    log.LogInformation("Onboarding request successfully posted for {NumberOfOnboardingsRequested} clients.", clients.Count());
}
