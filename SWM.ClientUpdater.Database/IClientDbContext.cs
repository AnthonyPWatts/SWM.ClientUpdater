using Microsoft.EntityFrameworkCore;

namespace SWM.ClientUpdater.Database;

public interface IClientDbContext
{
    internal DbSet<ClientDbObject> Clients { get; set; }

    internal Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
