# ---- build ----
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

# Project files first so restore is cached until a csproj changes
COPY src/RiffRecords.Domain/RiffRecords.Domain.csproj src/RiffRecords.Domain/
COPY src/RiffRecords.Application/RiffRecords.Application.csproj src/RiffRecords.Application/
COPY src/RiffRecords.Infrastructure/RiffRecords.Infrastructure.csproj src/RiffRecords.Infrastructure/
COPY src/RiffRecords.Api/RiffRecords.Api.csproj src/RiffRecords.Api/
RUN dotnet restore src/RiffRecords.Api/RiffRecords.Api.csproj

COPY src/ src/
RUN dotnet publish src/RiffRecords.Api/RiffRecords.Api.csproj -c Release -o /app/publish --no-restore

# ---- runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "RiffRecords.Api.dll"]