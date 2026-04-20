#Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "movie-reservation-system.csproj" -o publish /p:UseAppHost=false


#Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /src/publish .
ENTRYPOINT [ "dotnet", "movie-reservation-system.dll" ]

