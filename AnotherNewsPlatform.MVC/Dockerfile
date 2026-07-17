FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AnotherNewsPlatform.MVC/AnotherNewsPlatform.MVC.csproj", "AnotherNewsPlatform.MVC/"]
COPY ["AnotherNewsPlatform.Database/AnotherNewsPlatform.Database.csproj", "AnotherNewsPlatform.Database/"]
COPY ["AnotherNewsPlatform.ServiceDefaults/AnotherNewsPlatform.ServiceDefaults.csproj", "AnotherNewsPlatform.ServiceDefaults/"]
COPY ["AnotherNewsPlatform.CQRS/AnotherNewsPlatform.CQS.csproj", "AnotherNewsPlatform.CQRS/"]
COPY ["AnotherNewsPlatform.Core/AnotherNewsPlatform.Core.csproj", "AnotherNewsPlatform.Core/"]
COPY ["AnotherNewsPlatform.Services.ArticleService/AnotherNewsPlatform.Services.NewsService.csproj", "AnotherNewsPlatform.Services.ArticleService/"]
COPY ["AnotherNewsPlatform.Services.UserService/AnotherNewsPlatform.Services.UserService.csproj", "AnotherNewsPlatform.Services.UserService/"]
COPY ["AnotherNewsPlatform.SourceService/AnotherNewsPlatform.Services.SourceService.csproj", "AnotherNewsPlatform.SourceService/"]
COPY ["AnotherNewsPlatform.TokenService/AnotherNewsPlatform.TokenService.csproj", "AnotherNewsPlatform.TokenService/"]
RUN dotnet restore "AnotherNewsPlatform.MVC/AnotherNewsPlatform.MVC.csproj"
COPY . .
WORKDIR "/src/AnotherNewsPlatform.MVC"
RUN dotnet build "./AnotherNewsPlatform.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AnotherNewsPlatform.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AnotherNewsPlatform.MVC.dll"]
