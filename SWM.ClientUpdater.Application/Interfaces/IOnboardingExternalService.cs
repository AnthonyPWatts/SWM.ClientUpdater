using SWM.ClientUpdater.Application.Contracts;
using SWM.ClientUpdater.Application.DTOs;

namespace SWM.ClientUpdater.Application.Interfaces;

public interface IOnboardingExternalService
{
    public Task<ExternalServiceResult> PostOnboardingRequestAsync(IEnumerable<ClientDto> clients);
}
