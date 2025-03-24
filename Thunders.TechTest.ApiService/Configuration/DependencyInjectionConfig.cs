using Thunders.TechTest.ApiService.Data.Repositories;

namespace Thunders.TechTest.ApiService.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ITicketPedagioRepository, TicketPedagioRepository>();
    }
}