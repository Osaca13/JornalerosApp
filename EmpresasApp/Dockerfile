#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EmpresasApp/EmpresasApp.csproj", "EmpresasApp/"]
COPY ["JornalerosApp.Shared/JornalerosApp.Shared.csproj", "JornalerosApp.Shared/"]
COPY ["RabbitMQ/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "RabbitMQ/EventBusRabbitMQ/"]
RUN dotnet restore "EmpresasApp/EmpresasApp.csproj"
COPY . .
WORKDIR "/src/EmpresasApp"
RUN dotnet build "EmpresasApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmpresasApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmpresasApp.dll"]
