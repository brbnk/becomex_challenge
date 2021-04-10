#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["becomex_challange/becomex_challange.csproj", "becomex_challange/"]
RUN dotnet restore "becomex_challange/becomex_challange.csproj"
COPY . .
WORKDIR "/src/becomex_challange"
RUN dotnet build "becomex_challange.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "becomex_challange.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "becomex_challange.dll"]