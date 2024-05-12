using Microsoft.EntityFrameworkCore;
using SWM.ClientUpdater.Application.Enums;
using System;

namespace SWM.ClientUpdater.Database;

internal sealed class ClientDbContext : DbContext, IClientDbContext
{
    public ClientDbContext()
    {
    }

    public ClientDbContext(DbContextOptions<ClientDbContext> options)
        : base(options)
    {
    }

    public DbSet<ClientDbObject> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientDbObject>().HasKey(c => c.ID);
        modelBuilder.Entity<ClientDbObject>().ToTable("clients");

        modelBuilder.Entity<ClientDbObject>().Property(c => c.ID)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        modelBuilder.Entity<ClientDbObject>().Property(c => c.Name)
            .IsRequired()
            .HasColumnName("name");

        modelBuilder.Entity<ClientDbObject>().Property(c => c.Address)
            .IsRequired()
            .HasColumnName("address");

        modelBuilder.Entity<ClientDbObject>().Property(c => c.OnboardingStatus)
            .IsRequired()
            .HasColumnName("onboardingstatus");

        modelBuilder.Entity<ClientDbObject>().Property(c => c.JoinedDate)
            .IsRequired()
            .HasColumnName("joineddate");

        modelBuilder.Entity<ClientDbObject>().Property(c => c.MaritalStatus)
            .HasColumnName("maritalstatus")
            .HasConversion<string>();

    }

    // Need to explicitly implement this because it's an interface member
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}