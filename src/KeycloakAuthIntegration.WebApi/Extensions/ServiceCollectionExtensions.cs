using System.Reflection;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using KeycloakAuthIntegration.WebApi.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .ConfigureKeycloakAuth(configuration)
            .AddSwagger(configuration)
            .ConfigureCors(configuration);
        
        return services;
    }

    private static IServiceCollection ConfigureKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .ConfigureKeycloakAuth(configuration)
            .ConfigureKeycloakAuthorization(configuration);
        
        return services;
    }

    private static IServiceCollection ConfigureKeycloakAuth(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"{configuration["Keycloak:auth-server-url"]}/realms/{configuration["Keycloak:realm"]}",

                    ValidateAudience = true,
                    ValidAudience = "web-api",

                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false,

                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var client = new HttpClient();
                        var keyUri = $"{parameters.ValidIssuer}/protocol/openid-connect/certs";
                        var response = client.GetAsync(keyUri).Result;
                        var keys = new JsonWebKeySet(response.Content.ReadAsStringAsync().Result);

                        return keys.GetSigningKeys();
                    }
                };

                options.RequireHttpsMetadata = false; // Only for develop
                options.SaveToken = true;
            });
        
        services.AddHttpClient();
        
        return services;
    }

    private static IServiceCollection ConfigureKeycloakAuthorization(this IServiceCollection services,
        IConfiguration keycloakConfiguration)
    {
        services.AddKeycloakAuthorization(keycloakConfiguration);
        
        return services;
    }
    
    private static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Auth Web Web API",
                Description = ""
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter the token in the text box below.\nExample: 'your token here'"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        return services;
    }

    private static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Configuration.AllowProductManagementClient, policy =>
            {
                policy.WithOrigins(configuration.GetSection("AllowedClients:ClientApp").Value!)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        return services;
    }
}