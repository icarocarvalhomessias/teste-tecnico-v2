using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Data.Repositories;
using Thunders.TechTest.ApiService.Entities;

namespace Thunders.TechTest.ApiService.Data;

public class ThundersContext : DbContext, IUnitOfWork
{
    public ThundersContext(DbContextOptions<ThundersContext> options) 
        : base(options){}

    public DbSet<TicketPedagio> Tickets { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Pedagio> Pedagios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ThundersContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(name: "DataCriacao") != null))
        {
            if(entry.State == EntityState.Added)
            {
                entry.Property("DataCriacao").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCriacao").IsModified = false;
                entry.Property("DataUltimaAtualizacao").CurrentValue = DateTime.Now;
            }
        }

        return await base.SaveChangesAsync() > 0;
    }
}
