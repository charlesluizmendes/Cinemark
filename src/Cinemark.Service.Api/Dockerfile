#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Cinemark.Service.Api/Cinemark.Service.Api.csproj", "src/Cinemark.Service.Api/"]
COPY ["src/Cinemark.Infrastructure.IoC/Cinemark.Infrastructure.IoC.csproj", "src/Cinemark.Infrastructure.IoC/"]
COPY ["src/Cinemark.Application/Cinemark.Application.csproj", "src/Cinemark.Application/"]
COPY ["src/Cinemark.Domain/Cinemark.Domain.csproj", "src/Cinemark.Domain/"]
COPY ["src/Cinemark.Infrastructure.Data/Cinemark.Infrastructure.Data.csproj", "src/Cinemark.Infrastructure.Data/"]
COPY ["src/Cinemark.Infrastructure.EventBus/Cinemark.Infrastructure.EventBus.csproj", "src/Cinemark.Infrastructure.EventBus/"]
COPY ["src/Cinemark.Infrastructure.HostedServices/Cinemark.Infrastructure.HostedServices.csproj", "src/Cinemark.Infrastructure.HostedServices/"]
COPY ["src/Cinemark.Infrastructure.Identity/Cinemark.Infrastructure.Identity.csproj", "src/Cinemark.Infrastructure.Identity/"]
RUN dotnet restore "src/Cinemark.Service.Api/Cinemark.Service.Api.csproj"
COPY . .
WORKDIR "/src/src/Cinemark.Service.Api"
RUN dotnet build "Cinemark.Service.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cinemark.Service.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cinemark.Service.Api.dll"]