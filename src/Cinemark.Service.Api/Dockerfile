#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cinemark.Service.Api/Cinemark.Service.Api.csproj", "Cinemark.Service.Api/"]
COPY ["Cinemark.Infrastructure.IoC/Cinemark.Infrastructure.IoC.csproj", "Cinemark.Infrastructure.IoC/"]
COPY ["Cinemark.Domain/Cinemark.Domain.csproj", "Cinemark.Domain/"]
COPY ["Cinemark.Infrastructure.Identity/Cinemark.Infrastructure.Identity.csproj", "Cinemark.Infrastructure.Identity/"]
COPY ["Cinemark.Application/Cinemark.Application.csproj", "Cinemark.Application/"]
COPY ["Cinemark.Infrastructure.Data/Cinemark.Infrastructure.Data.csproj", "Cinemark.Infrastructure.Data/"]
RUN dotnet restore "Cinemark.Service.Api/Cinemark.Service.Api.csproj"
COPY . .
WORKDIR "/src/Cinemark.Service.Api"
RUN dotnet build "Cinemark.Service.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cinemark.Service.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cinemark.Service.Api.dll"]