FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/Catalog/API/Catalog.API.csproj", "Services/Catalog/API/"]
COPY ["src/Services/Catalog/Domain/Catalog.Domain.csproj", "Services/Catalog/Domain/"]
COPY ["src/Services/Catalog/Infrastructure/Catalog.Infrastructure.csproj", "Services/Catalog/Infrastructure/"]
RUN dotnet restore "Services/Catalog/API/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/API"
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
