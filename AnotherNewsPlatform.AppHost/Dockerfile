FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AnotherNewsPlatform.AppHost/AnotherNewsPlatform.AppHost.csproj", "AnotherNewsPlatform.AppHost/"]
COPY ["AnotherNewsPlatform.MVC/AnotherNewsPlatform.MVC.csproj", "AnotherNewsPlatform.MVC/"]
COPY ["AnotherNewsPlatform.Database/AnotherNewsPlatform.Database.csproj", "AnotherNewsPlatform.Database/"]
COPY ["AnotherNewsPlatform.ServiceDefaults/AnotherNewsPlatform.ServiceDefaults.csproj", "AnotherNewsPlatform.ServiceDefaults/"]
COPY ["AnotherNewsPlatform.CQRS/AnotherNewsPlatform.CQS.csproj", "AnotherNewsPlatform.CQRS/"]
COPY ["AnotherNewsPlatform.Core/AnotherNewsPlatform.Core.csproj", "AnotherNewsPlatform.Core/"]
COPY ["AnotherNewsPlatform.Services.ArticleService/AnotherNewsPlatform.Services.NewsService.csproj", "AnotherNewsPlatform.Services.ArticleService/"]
COPY ["AnotherNewsPlatform.Services.UserService/AnotherNewsPlatform.Services.UserService.csproj", "AnotherNewsPlatform.Services.UserService/"]
COPY ["AnotherNewsPlatform.SourceService/AnotherNewsPlatform.Services.SourceService.csproj", "AnotherNewsPlatform.SourceService/"]
COPY ["AnotherNewsPlatform.TokenService/AnotherNewsPlatform.TokenService.csproj", "AnotherNewsPlatform.TokenService/"]
COPY ["AnotherNewsPlatform.WebApi/AnotherNewsPlatform.WebApi.csproj", "AnotherNewsPlatform.WebApi/"]
RUN dotnet restore "AnotherNewsPlatform.AppHost/AnotherNewsPlatform.AppHost.csproj"
COPY . .
WORKDIR "/src/AnotherNewsPlatform.AppHost"
RUN dotnet build "./AnotherNewsPlatform.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AnotherNewsPlatform.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnotherNewsPlatform.AppHost.dll"]
