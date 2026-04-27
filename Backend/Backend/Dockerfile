#back
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build_back
WORKDIR /src

COPY Backend.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish "Backend.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build_back /app/publish .

ENTRYPOINT ["dotnet", "Backend.dll"]