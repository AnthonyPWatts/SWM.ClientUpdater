using SWM.ClientUpdater.Application.Contracts;
using SWM.ClientUpdater.Application.DTOs;
using SWM.ClientUpdater.Application.Interfaces;
using System.Text;

namespace SWM.ClientUpdater.ExternalServices;

public sealed class OnboardingExternalService : IOnboardingExternalService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _externalServiceEndpoint;
    public OnboardingExternalService(IHttpClientFactory httpClientFactory, string externalServiceEndpoint)
    {
        _httpClientFactory = httpClientFactory;
        _externalServiceEndpoint = externalServiceEndpoint;
    }

    public async Task<ExternalServiceResult> PostOnboardingRequestAsync(IEnumerable<ClientDto> clients)
    {
        // this all seems very weird. Why would an API want data sent to it in this format?!
        // personally I'd be expecting to serialise the collection to a JSON array and POST that
        using var content = GenerateStringContent(clients);

        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.PostAsync(_externalServiceEndpoint, content);

        return response.IsSuccessStatusCode
            ? new ExternalServiceResult(true, "Onboarding request successfully posted.")
            : new ExternalServiceResult(false, $"Failed to post onboarding request. API response code: {response.StatusCode}. API response: {await response.Content.ReadAsStringAsync()}");
    }

    private static string ToCsvLine(ClientDto client)
    {
        var address = client.Address?.ToSingleLine() ?? "";
        return $"{client.UserId},{client.Name},{client.OnboardingStatus},{client.JoinedDate},{client.MaritalStatus},{address}";
    }

    private static string GenerateAllClientsCsv(IEnumerable<ClientDto> clients)
    {
        var csvBuilder = new StringBuilder();

        foreach (var client in clients)
        {
            csvBuilder.AppendLine(ToCsvLine(client));
        }

        return csvBuilder.ToString();
    }

    private static MultipartFormDataContent GenerateStringContent(IEnumerable<ClientDto> clients)
    {
        return new MultipartFormDataContent
        {
            { 
                new StringContent(GenerateAllClientsCsv(clients)), 
                "pending_clients", 
                "pending_clients.csv" 
            }
        };
    }
}
