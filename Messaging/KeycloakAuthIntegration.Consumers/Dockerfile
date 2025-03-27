FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Messaging/KeycloakAuthIntegration.Consumers/KeycloakAuthIntegration.Consumers.csproj", "Messaging/KeycloakAuthIntegration.Consumers/"]
COPY ["Messaging/KeycloakAuthIntegration.Messaging.Domain/KeycloakAuthIntegration.Messaging.Domain.csproj", "Messaging/KeycloakAuthIntegration.Messaging.Domain/"]
COPY ["Messaging/KeycloakAuthIntegration.Messaging.ORM/KeycloakAuthIntegration.Messaging.ORM.csproj", "Messaging/KeycloakAuthIntegration.Messaging.ORM/"]
COPY ["src/KeycloakAuthIntegration.Messaging/KeycloakAuthIntegration.Messaging.csproj", "src/KeycloakAuthIntegration.Messaging/"]
COPY ["Messaging/KeycloakAuthIntegration.Messaging.Common/KeycloakAuthIntegration.Messaging.Common.csproj", "Messaging/KeycloakAuthIntegration.Messaging.Common/"]
RUN dotnet restore "Messaging/KeycloakAuthIntegration.Consumers/KeycloakAuthIntegration.Consumers.csproj"
COPY . .
WORKDIR "/src/Messaging/KeycloakAuthIntegration.Consumers"
RUN dotnet build "KeycloakAuthIntegration.Consumers.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KeycloakAuthIntegration.Consumers.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KeycloakAuthIntegration.Consumers.dll"]
