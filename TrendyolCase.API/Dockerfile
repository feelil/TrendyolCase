#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TrendyolCase.API/TrendyolCase.API.csproj", "TrendyolCase.API/"]
COPY ["TrendyolCase.DataAccess/TrendyolCase.DataAccess.csproj", "TrendyolCase.DataAccess/"]
RUN dotnet restore "TrendyolCase.API/TrendyolCase.API.csproj"
COPY . .
WORKDIR "/src/TrendyolCase.API"
RUN dotnet build "TrendyolCase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrendyolCase.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrendyolCase.API.dll"]