# Obtener imagen base para la ejecucion del api
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# Obtener imagen sdk para compilacion y publicacion del api
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY . .

WORKDIR /app/api
RUN dotnet restore

RUN dotnet build "api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "api.csproj" -c Release -o /app/publish

# Publicar api en imagen base
FROM base as final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "api.dll" ]
