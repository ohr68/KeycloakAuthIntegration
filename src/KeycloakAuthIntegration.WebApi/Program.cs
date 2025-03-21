using KeycloakAuthIntegration.IoC;
using KeycloakAuthIntegration.IoC.HealthChecks;
using KeycloakAuthIntegration.IoC.Logging;
using Serilog;
using WebApi.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddProblemDetails(options =>
                options.CustomizeProblemDetails = ctx =>
                {
                    ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
                    ctx.ProblemDetails.Extensions.Add("instance",
                        $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
                });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.AddBasicHealthChecks();
            builder.Services.ConfigureServices(builder.Configuration, builder.Environment.IsDevelopment());
            builder.Services.ConfigureKeycloak(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseBasicHealthChecks();
            app.MapControllers().RequireAuthorization();
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
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