FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY DataWareHouse.sln ./
COPY src/Services/Game/Game.API/Game.API.csproj src/Services/Game/Game.API/
COPY src/BuildingBlocks/HealthChecks/AspNetCore.HealthChecks/AspNetCore.HealthChecks.csproj src/BuildingBlocks/HealthChecks/src/AspNetCore.HealthChecks/
COPY src/BuildingBlocks/EventBus/IntegrationEventLog/IntegrationEventLog.csproj src/BuildingBlocks/EventBus/IntegrationEventLog/
COPY src/BuildingBlocks/EventBus/EventBus/EventBus.csproj src/BuildingBlocks/EventBus/EventBus/
COPY src/BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj src/BuildingBlocks/EventBus/EventBusRabbitMQ/
COPY src/Services/Game/Game.Domain/Game.Domain.csproj src/Services/Game/Game.Domain/
COPY src/BuildingBlocks/EventBus/EventBusServiceBus/EventBusServiceBus.csproj src/BuildingBlocks/EventBus/EventBusServiceBus/
COPY src/BuildingBlocks/WebHost/WebHost.Customization/WebHost.Customization.csproj src/BuildingBlocks/WebHost/WebHost.Customization/
COPY src/Services/Game/Game.Infrastructure/Game.Infrastructure.csproj src/Services/Game/Game.Infrastructure/
COPY src/BuildingBlocks/HealthChecks/Postgresql.HealthChecks/Postgresql.HealthChecks.csproj src/BuildingBlocks/HealthChecks/Postgresql.HealthChecks/
RUN dotnet restore
COPY . .
WORKDIR /src/src/Services/Game/Game.API
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Game.API.dll"]
