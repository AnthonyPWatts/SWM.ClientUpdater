using SWM.ClientUpdater.Application.DTOs;

namespace SWM.ClientUpdater.Application.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<ClientDto>> GetOnboardOutstandingClientsAsync(int joinedAtLeastDaysAgo);
}
