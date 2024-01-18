FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Authentication/Authentication.IdentityServer/Authentication.IdentityServer.csproj", "src/Authentication/Authentication.IdentityServer/"]
RUN dotnet restore "src/Authentication/Authentication.IdentityServer/Authentication.IdentityServer.csproj"
COPY . .
WORKDIR "/src/src/Authentication/Authentication.IdentityServer"
RUN dotnet build "Authentication.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Authentication.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authentication.IdentityServer.dll"]
