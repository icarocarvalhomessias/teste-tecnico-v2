using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using Thunders.TechTest.ApiService.Data;

namespace Thunders.TechTest.ApiService.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ThundersContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        var pack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", pack, t => true);

        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetSection("MongoDB:ConnectionString").Value;
            return new MongoClient(connectionString);
        });

        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var configuration = sp.GetRequiredService<IConfiguration>();
            var databaseName = configuration.GetSection("MongoDB:DatabaseName").Value;
            return client.GetDatabase(databaseName);
        });

        services.AddControllers();

        services.AddSwaggerConfiguration();

        services.AddExceptionHandler(options =>
        {
            options.ExceptionHandler = async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
            };
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApiConfig).Assembly));
        services.AddAutoMapper(typeof(MappingProfile));

        services.RegisterServices();

        return services;
    }
}