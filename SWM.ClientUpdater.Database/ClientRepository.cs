using Microsoft.EntityFrameworkCore;
using SWM.ClientUpdater.Application.DTOs;
using SWM.ClientUpdater.Application.Enums;
using SWM.ClientUpdater.Application.Interfaces;

namespace SWM.ClientUpdater.Database;

internal class ClientRepository : IClientRepository
{
    private readonly IClientDbContext _clientDbContext;

    public ClientRepository(IClientDbContext clientDbContext)
    {
        _clientDbContext = clientDbContext;
    }
    
    public async Task<IEnumerable<ClientDto>> GetOnboardOutstandingClientsAsync(int joinedAtLeastDaysAgo)
    {
        try
        {
            return await _clientDbContext.Clients
                .Where(c =>
                    c.JoinedDate <= DateTime.UtcNow.Date.AddDays(-joinedAtLeastDaysAgo) &&
                    c.OnboardingStatus == OnboardingStatus.Pending.ToString())
                .Select(x => x.ToDto())
                .ToListAsync();
        }
        catch (Exception ex) {
            throw new Exception("Error getting clients", ex);
        }
    }
}
