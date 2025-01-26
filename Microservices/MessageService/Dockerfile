# ビルドステージ
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# csprojとnugetパッケージの復元
COPY MessageService/*.csproj ./MessageService/
RUN dotnet restore MessageService/MessageService.csproj

# ソースコードをコピーしてビルド
COPY . .
WORKDIR /app/Microservices/MessageService
RUN dotnet publish -c Release -o out

# ランタイムステージ
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/Microservices/MessageService/out ./
ENTRYPOINT ["dotnet", "MessageService.dll"]