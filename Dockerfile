#back
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build_back
WORKDIR /src

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build_back /app .

ENTRYPOINT ["dotnet", "Backend.dll"]

#front
FROM node:20 as build_front
WORKDIR /app
COPY . .
RUN npm install && npm run build

FROM nginx
COPY --from=build_front /app/dist /usr/share/nginx/html