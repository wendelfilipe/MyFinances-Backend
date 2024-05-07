# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /Backend

COPY *.sln .
COPY Backend.API/*.csproj ./Backend.API/
COPY Backend.Application/*.csproj ./Backend.Application/
COPY Backend.Domain/*.csproj ./Backend.Domain/
COPY Backend.Infra.Data/*.csproj ./Backend.Infra.Data/
COPY Backend.Infra.Ioc/*.csproj ./Backend.Infra.Ioc/
COPY Backend.Domain.Tests/*.csproj ./Backend.Domain.Tests/
RUN dotnet restore
WORKDIR /Backend/Backend.API
RUN dotnet build "Backend.API.csproj" -c release

# copy everything else and build app
COPY ./. ././
WORKDIR /Backend
RUN dotnet publish -c release --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /Backend
COPY ./Backend.API/bin/Debug/net8.0/ ./
ENV ASPNETCORE_URLS=http://0.0.0.0:3000
ENTRYPOINT ["dotnet", "Backend.API.dll"]