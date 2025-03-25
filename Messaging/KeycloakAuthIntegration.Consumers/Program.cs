using KeycloakAuthIntegration.Consumers.Extensions;
using KeycloakAuthIntegration.Messaging.Common.Logging;
using KeycloakAuthIntegration.Messaging.ORM.Context;
using Serilog;

namespace KeycloakAuthIntegration.Consumers;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting UserCreated Consumer");

            var builder = Host.CreateApplicationBuilder(args);

            builder.AddDefaultLogging();
            builder.Services.AddServices(builder.Configuration, builder.Environment.IsDevelopment());
            
            var host = builder.Build();
            host.UseDefaultLogging();
            
            // When the app runs, it first creates the Database.
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MessagingDbContext>();
            context.Database.EnsureCreated();
            
            host.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
            Console.WriteLine($"Critical error: {ex.Message}");
            Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            Console.WriteLine(ex.StackTrace);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}