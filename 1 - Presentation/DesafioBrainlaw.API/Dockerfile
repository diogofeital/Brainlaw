#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM nginx
COPY "1 - Presentation/DesafioBrainlaw.Front/" /usr/share/nginx/html
RUN chmod +r /usr/share/nginx/html/index.html

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["1 - Presentation/DesafioBrainlaw.API/DesafioBrainlaw.API.csproj", "1 - Presentation/DesafioBrainlaw.API/"]
COPY ["2 - Application/DesafioBrainlaw.Application/DesafioBrainlaw.Application.csproj", "2 - Application/DesafioBrainlaw.Application/"]
COPY ["3 - Domain/DesafioBrainlaw.Domain/DesafioBrainlaw.Domain.csproj", "3 - Domain/DesafioBrainlaw.Domain/"]
COPY ["3 - Domain/DesafioBrainlaw.Domain.Shared/DesafioBrainlaw.Domain.Shared.csproj", "3 - Domain/DesafioBrainlaw.Domain.Shared/"]
COPY ["4 - Infra/DesafioBrainlaw.Infrastructure/DesafioBrainlaw.Infrastructure.csproj", "4 - Infra/DesafioBrainlaw.Infrastructure/"]
COPY ["4 - Infra/DesafioBrainlaw.Infra.CrossCutting.IoC/DesafioBrainlaw.Infra.CrossCutting.IoC.csproj", "4 - Infra/DesafioBrainlaw.Infra.CrossCutting.IoC/"]
RUN dotnet restore "1 - Presentation/DesafioBrainlaw.API/DesafioBrainlaw.API.csproj"
COPY . .
WORKDIR "/src/1 - Presentation/DesafioBrainlaw.API"
RUN dotnet build "DesafioBrainlaw.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioBrainlaw.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioBrainlaw.API.dll", "nginx", "-g", "-d"]