#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BookApp.Web.V2/BookApp.Web.csproj", "BookApp.Web.V2/"]
COPY ["BookApp.DAL/BookApp.DAL.csproj", "BookApp.DAL/"]
COPY ["Models/BookApp.Models.csproj", "Models/"]
RUN dotnet restore "BookApp.Web.V2/BookApp.Web.csproj"
COPY . .
WORKDIR "/src/BookApp.Web.V2"
RUN dotnet build "BookApp.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookApp.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookApp.Web.dll"]
