using SWM.ClientUpdater.Application.Contracts;
using SWM.ClientUpdater.Application.DTOs;
using SWM.ClientUpdater.Application.Interfaces;

namespace SWM.ClientUpdater.Application.Services;

internal class ClientService : IClientService
{
    private readonly IOnboardingExternalService _onboardingService;
    private readonly IClientRepository _clientRepository;   
    public ClientService(IOnboardingExternalService onboadingService, IClientRepository clientRepository)
    {
        _onboardingService = onboadingService;
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<ClientDto>> GetOnboardOutstandingClientsAsync(int joinedAtLeastDaysAgo)
    {
        return await _clientRepository.GetOnboardOutstandingClientsAsync(joinedAtLeastDaysAgo);
    }

    public async Task<ExternalServiceResult> RequestOnboardingAsync(IEnumerable<ClientDto> clients)
    {
        return await _onboardingService.PostOnboardingRequestAsync(clients);
    }
}
