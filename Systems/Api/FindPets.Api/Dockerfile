#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systems/Api/FindPets.Api/FindPets.Api.csproj", "Systems/Api/FindPets.Api/"]
COPY ["Services/FindPets.Services.Settings/FindPets.Services.Settings.csproj", "Services/FindPets.Services.Settings/"]
COPY ["Shared/FindPets.Settings/FindPets.Settings.csproj", "Shared/FindPets.Settings/"]
COPY ["Shared/FindPets.Common/FindPets.Common.csproj", "Shared/FindPets.Common/"]
COPY ["Systems/Api/FindPets.Api.Settings/FindPets.Api.Settings.csproj", "Systems/Api/FindPets.Api.Settings/"]
RUN dotnet restore "Systems/Api/FindPets.Api/FindPets.Api.csproj"
COPY . .
WORKDIR "/src/Systems/Api/FindPets.Api"
RUN dotnet build "FindPets.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindPets.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FindPets.Api.dll"]