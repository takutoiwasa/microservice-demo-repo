# ビルドステージ
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# csprojとnugetパッケージの復元
COPY ItemManagementService/*.csproj ./ItemManagementService/
RUN dotnet restore ItemManagementService/ItemManagementService.csproj

# ソースコードをコピーしてビルド
COPY . .
WORKDIR /app/Microservices/ItemManagementService
RUN dotnet publish -c Release -o out

# ランタイムステージ
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/Microservices/ItemManagementService/out ./
ENTRYPOINT ["dotnet", "ItemManagementService.dll"]