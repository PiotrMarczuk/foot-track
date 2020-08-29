FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app/back

COPY *.csproj ./
RUN dotnet restore

COPY . ./
ARG Environment=Debug
RUN dotnet publish -c ${Environment} -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app/back
COPY --from=build-env /app/back/out .
ENTRYPOINT ["dotnet", "FootTrack.Api.dll"]
