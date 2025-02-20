#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Pacagroup.Ecommerce.Services.WebApi/Pacagroup.Ecommerce.Services.WebApi.csproj", "Pacagroup.Ecommerce.Services.WebApi/"]
COPY ["Pacagroup.Ecommerce.Application.Interface/Pacagroup.Ecommerce.Application.Interface.csproj", "Pacagroup.Ecommerce.Application.Interface/"]
COPY ["Pacagroup.Ecommerce.Transversal.Common/Pacagroup.Ecommerce.Transversal.Common.csproj", "Pacagroup.Ecommerce.Transversal.Common/"]
COPY ["Packagroup.Ecommerce.Applicaion.DTO/Pacagroup.Ecommerce.Application.DTO.csproj", "Packagroup.Ecommerce.Applicaion.DTO/"]
COPY ["Pacagroup.Ecommerce.Application.Main/Pacagroup.Ecommerce.Application.Main.csproj", "Pacagroup.Ecommerce.Application.Main/"]
COPY ["Pacagroup.Ecommerce.Domain.Entity/Pacagroup.Ecommerce.Domain.Entity.csproj", "Pacagroup.Ecommerce.Domain.Entity/"]
COPY ["Pacagroup.Ecommerce.Domain.Interface/Pacagroup.Ecommerce.Domain.Interface.csproj", "Pacagroup.Ecommerce.Domain.Interface/"]
COPY ["Pacagroup.Ecommerce.Domain.Core/Pacagroup.Ecommerce.Domain.Core.csproj", "Pacagroup.Ecommerce.Domain.Core/"]
COPY ["Pacagroup.Ecommerce.Infraestructure.Interface/Pacagroup.Ecommerce.Infraestructure.Interface.csproj", "Pacagroup.Ecommerce.Infraestructure.Interface/"]
COPY ["Pacagroup.Ecommerce.Infraestructure.Data/Pacagroup.Ecommerce.Infraestructure.Data.csproj", "Pacagroup.Ecommerce.Infraestructure.Data/"]
COPY ["Pacagroup.Ecommerce.Infraestructure.Repository/Pacagroup.Ecommerce.Infraestructure.Repository.csproj", "Pacagroup.Ecommerce.Infraestructure.Repository/"]
COPY ["Pacagroup.Ecommerce.Transversal.Logging/Pacagroup.Ecommerce.Transversal.Logging.csproj", "Pacagroup.Ecommerce.Transversal.Logging/"]
COPY ["Packagroup.Ecommerce.Transversal.Mapper/Pacagroup.Ecommerce.Transversal.Mapper.csproj", "Packagroup.Ecommerce.Transversal.Mapper/"]
RUN dotnet restore "./Pacagroup.Ecommerce.Services.WebApi/Pacagroup.Ecommerce.Services.WebApi.csproj"
COPY . .
WORKDIR "/src/Pacagroup.Ecommerce.Services.WebApi"
RUN dotnet build "./Pacagroup.Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Pacagroup.Ecommerce.Services.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pacagroup.Ecommerce.Services.WebApi.dll"]