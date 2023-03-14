#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/RuneSharper.Worker/RuneSharper.Worker.csproj", "src/RuneSharper.Worker/"]
COPY ["src/RuneSharper.IoC/RuneSharper.IoC.csproj", "src/RuneSharper.IoC/"]
COPY ["src/RuneSharper.Application/RuneSharper.Application.csproj", "src/RuneSharper.Application/"]
COPY ["src/RuneSharper.Domain/RuneSharper.Domain.csproj", "src/RuneSharper.Domain/"]
COPY ["src/RuneSharper.Shared/RuneSharper.Shared.csproj", "src/RuneSharper.Shared/"]
COPY ["src/RuneSharper.Data/RuneSharper.Data.csproj", "src/RuneSharper.Data/"]
RUN dotnet restore "src/RuneSharper.Worker/RuneSharper.Worker.csproj"
COPY . .
WORKDIR "/src/src/RuneSharper.Worker"
RUN dotnet build "RuneSharper.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RuneSharper.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RuneSharper.Worker.dll"]