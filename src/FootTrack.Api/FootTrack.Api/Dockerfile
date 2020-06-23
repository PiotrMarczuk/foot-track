FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore

COPY . ./
FROM build AS publish
RUN dotnet test -c Release
RUN dotnet publish -c Release -o out

FROM base AS final
WORKDIR /app
COPY --from=publish /app/out .
ENTRYPOINT ["dotnet", "FootTrack.Api.dll"]
