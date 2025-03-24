using MongoDB.Driver;
using System.Net;
using Thunders.TechTest.ApiService.Configuration;
using Thunders.TechTest.ApiService.Data.MongoDb;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var hostEnvironment = builder.Environment;

        builder.Configuration
            .SetBasePath(hostEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        builder.AddServiceDefaults();

        builder.Services.AddApiConfiguration(builder.Configuration);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseSwaggerConfiguration();

        app.MapDefaultEndpoints();
        app.MapControllers();

        // Configure MongoDB Indexes
        using (var scope = app.Services.CreateScope())
        {
            var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
            var indexConfigurator = new MongoDbIndexConfigurator(database);
            indexConfigurator.ConfigureIndexes();
        }

        app.Run();
    }
}
