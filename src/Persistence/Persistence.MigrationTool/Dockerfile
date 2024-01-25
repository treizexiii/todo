FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Persistence/Persistance.MigrationTool/Persistance.MigrationTool.csproj", "src/Persistence/Persistance.MigrationTool/"]
RUN dotnet restore "src/Persistence/Persistance.MigrationTool/Persistance.MigrationTool.csproj"
COPY . .
WORKDIR "/src/src/Persistence/Persistance.MigrationTool"
RUN dotnet build "Persistance.MigrationTool.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Persistance.MigrationTool.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Persistance.MigrationTool.dll"]
