using Microsoft.OpenApi.Models;

namespace Thunders.TechTest.ApiService.Configuration;


public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "My API",
                Description = "API Documentation"
            });
            c.EnableAnnotations();
        });
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthorization();

        // Configura o Swagger
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty; // Para abrir o Swagger na raiz
        });
     
    }
}
