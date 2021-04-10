FROM mcr.microsoft.com/dotnet/sdk:5.0 as builder
WORKDIR /app

COPY *.sln .
COPY BecomexChallenge.API/*.csproj ./BecomexChallenge.API/
COPY BecomexChallenge.Business/*.csproj ./BecomexChallenge.Business/
COPY BecomexChallenge.Domain/*.csproj ./BecomexChallenge.Domain/
RUN dotnet restore

COPY BecomexChallenge.API/. ./BecomexChallenge.API/
COPY BecomexChallenge.Business/. ./BecomexChallenge.Business/
COPY BecomexChallenge.Domain/. ./BecomexChallenge.Domain/

RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=builder /app ./

EXPOSE 5000

ENTRYPOINT [ "dotnet", "BecomexChallenge.Api.dll" ]