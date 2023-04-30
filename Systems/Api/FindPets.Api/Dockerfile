#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systems/Api/FindPets.Api/FindPets.Api.csproj", "Systems/Api/FindPets.Api/"]
COPY ["Data/FindPets.Context/FindPets.Context.csproj", "Data/FindPets.Context/"]
COPY ["Shared/FindPets.Settings/FindPets.Settings.csproj", "Shared/FindPets.Settings/"]
COPY ["Data/FindPets.Context.Entities/FindPets.Context.Entities.csproj", "Data/FindPets.Context.Entities/"]
COPY ["Data/Migrations/FindPets.Context.MigrationsMSSQL/FindPets.Context.MigrationsMSSQL.csproj", "Data/Migrations/FindPets.Context.MigrationsMSSQL/"]
COPY ["Data/Migrations/FindPets.Context.MigrationsPostgreSQL/FindPets.Context.MigrationsPostgreSQL.csproj", "Data/Migrations/FindPets.Context.MigrationsPostgreSQL/"]
COPY ["Services/FindPets.Services.Animals/FindPets.Services.Animals.csproj", "Services/FindPets.Services.Animals/"]
COPY ["Shared/FindPets.Common/FindPets.Common.csproj", "Shared/FindPets.Common/"]
COPY ["Services/FindPets.Services.EmailSender/FindPets.Services.EmailSender.csproj", "Services/FindPets.Services.EmailSender/"]
COPY ["Services/FindPets.Services.Comments/FindPets.Services.Comments.csproj", "Services/FindPets.Services.Comments/"]
COPY ["Services/FindPets.Services.Requests/FindPets.Services.Requests.csproj", "Services/FindPets.Services.Requests/"]
COPY ["Services/FindPets.Services.Settings/FindPets.Services.Settings.csproj", "Services/FindPets.Services.Settings/"]
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