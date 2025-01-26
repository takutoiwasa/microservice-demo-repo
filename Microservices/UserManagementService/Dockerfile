# ビルドステージ
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# csprojとnugetパッケージの復元
COPY UserManagementService/*.csproj ./UserManagementService/
RUN dotnet restore UserManagementService/UserManagementService.csproj

# ソースコードをコピーしてビルド
COPY . .
WORKDIR /app/Microservices/UserManagementService
RUN dotnet publish -c Release -o out

# ランタイムステージ
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/Microservices/UserManagementService/out ./
ENTRYPOINT ["dotnet", "UserManagementService.dll"]