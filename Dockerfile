# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /Backend

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Backend.API/*.csproj ./Backend.API/
COPY Backend.Application/*.csproj ./Backend.Application/
COPY Backend.Domain/*.csproj ./Backend.Domain/
COPY Backend.Infra.Data/*.csproj ./Backend.Infra.Data/
COPY Backend.Infra.Ioc/*.csproj ./Backend.Infra.Ioc/
COPY Backend.WebUI/*.csproj ./Backend.WebUI/
COPY Backend.Domain.Tests/*.csproj ./Backend.Domain.Tests/
RUN dotnet restore

# copy everything else and build app
COPY ./. ././
WORKDIR /Backend
RUN dotnet publish -c release --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /Backend
COPY --from=build /Backend ./
ENTRYPOINT ["dotnet", "Backend.API.dll"]