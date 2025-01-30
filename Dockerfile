# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RivalTranslator.csproj", "./"]
RUN dotnet restore "./RivalTranslator.csproj"

COPY . .
WORKDIR "/src/"
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "RivalTranslator.dll"]
