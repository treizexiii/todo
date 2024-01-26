FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Persistence/Persistence.MigrationTool/Persistence.MigrationTool.csproj", "src/Persistence/Persistence.MigrationTool/"]
RUN dotnet restore "src/Persistence/Persistence.MigrationTool/Persistence.MigrationTool.csproj"
COPY . .
WORKDIR "/src/src/Persistence/Persistence.MigrationTool"
RUN dotnet build "Persistence.MigrationTool.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Persistence.MigrationTool.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Persistence.MigrationTool.dll"]
