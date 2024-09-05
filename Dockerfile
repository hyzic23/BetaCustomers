# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./BetaCustomers.API/BetaCustomers.API.csproj" --disable-parallel
RUN dotnet publish "./BetaCustomers.API/BetaCustomers.API.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "BetaCustomers.API.dll"]