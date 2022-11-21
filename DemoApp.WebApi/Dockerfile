#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DemoApp.WebApi/DemoApp.WebApi.csproj", "DemoApp.WebApi/"]
COPY ["DemoApp.Infrastructure.Persistence/DemoApp.Infrastructure.Persistence.csproj", "DemoApp.Infrastructure.Persistence/"]
COPY ["DemoApp.Application/DemoApp.Application.csproj", "DemoApp.Application/"]
COPY ["DemoApp.Domain/DemoApp.Domain.csproj", "DemoApp.Domain/"]
COPY ["DemoApp.Infrastructure.Shared/DemoApp.Infrastructure.Shared.csproj", "DemoApp.Infrastructure.Shared/"]
RUN dotnet restore "DemoApp.WebApi/DemoApp.WebApi.csproj"
COPY . .
WORKDIR "/src/DemoApp.WebApi"
RUN dotnet build "DemoApp.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoApp.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoApp.WebApi.dll"]