#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["JornalerosApp/JornalerosApp.csproj", "JornalerosApp/"]
COPY ["JornalerosApp.Shared/JornalerosApp.Shared.csproj", "JornalerosApp.Shared/"]
COPY ["JornalerosApp.Application/JornalerosApp.Application/JornalerosApp.Application.csproj", "JornalerosApp.Application/JornalerosApp.Application/"]
COPY ["JornalerosApp.Infrastructure/JornalerosApp.Infrastructure.csproj", "JornalerosApp.Infrastructure/"]
COPY ["RabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "RabbitMQ/EventBusRabbitMQ/"]
RUN dotnet restore "JornalerosApp/JornalerosApp.csproj"
COPY . .
WORKDIR "/src/JornalerosApp"
RUN dotnet build "JornalerosApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JornalerosApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JornalerosApp.dll"]
