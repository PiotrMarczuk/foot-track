FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FootTrack.Api/FootTrack.Api.csproj", "FootTrack.Api/"]
COPY ["FootTrack.BusinessLogic.Models/FootTrack.BusinessLogic.Models.csproj", "FootTrack.BusinessLogic.Models/"]
COPY ["FootTrack.Shared/FootTrack.Shared.csproj", "FootTrack.Shared/"]
COPY ["FootTrack.Repository/FootTrack.Repository.csproj", "FootTrack.Repository/"]
COPY ["FootTrack.Database/FootTrack.Database.csproj", "FootTrack.Database/"]
COPY ["FootTrack.Settings/FootTrack.Settings.csproj", "FootTrack.Settings/"]
COPY ["FootTrack.BusinessLogic/FootTrack.BusinessLogic.csproj", "FootTrack.BusinessLogic/"]
RUN dotnet restore "FootTrack.Api/FootTrack.Api.csproj"
COPY . .
WORKDIR "/src/FootTrack.Api"
ARG Environment=Debug
RUN dotnet build "FootTrack.Api.csproj" -c ${Environment} -o /app/build

FROM build AS publish
ARG Environment=Debug
RUN dotnet publish "FootTrack.Api.csproj" -c ${Environment} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FootTrack.Api.dll"]