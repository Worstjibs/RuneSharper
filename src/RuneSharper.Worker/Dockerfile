#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RuneSharper.Worker/RuneSharper.Worker.csproj", "RuneSharper.Worker/"]
COPY ["RuneSharper.Services/RuneSharper.Services.csproj", "RuneSharper.Services/"]
COPY ["RuneSharper.Data/RuneSharper.Data.csproj", "RuneSharper.Data/"]
COPY ["RuneSharper.Shared/RuneSharper.Shared.csproj", "RuneSharper.Shared/"]
RUN dotnet restore "RuneSharper.Worker/RuneSharper.Worker.csproj"
COPY . .
WORKDIR "/src/RuneSharper.Worker"
RUN dotnet build "RuneSharper.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RuneSharper.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RuneSharper.Worker.dll"]