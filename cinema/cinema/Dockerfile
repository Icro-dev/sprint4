# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# copy .env
COPY .env .

# Copy csproj and restore as distinct layers
COPY cinema/cinema/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY cinema/cinema ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "cinema.dll"]
