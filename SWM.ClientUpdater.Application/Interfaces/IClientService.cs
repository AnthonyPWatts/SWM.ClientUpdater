using SWM.ClientUpdater.Application.Contracts;
using SWM.ClientUpdater.Application.DTOs;

namespace SWM.ClientUpdater.Application.Interfaces;

public interface IClientService
{
    public Task<IEnumerable<ClientDto>> GetOnboardOutstandingClientsAsync(int joinedAtLeastDaysAgo);
    public Task<ExternalServiceResult> RequestOnboardingAsync(IEnumerable<ClientDto> clients);
}
