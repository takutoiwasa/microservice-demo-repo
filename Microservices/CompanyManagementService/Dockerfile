# ビルドステージ
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# csprojとnugetパッケージの復元
COPY CompanyManagementService/*.csproj ./CompanyManagementService/
RUN dotnet restore CompanyManagementService/CompanyManagementService.csproj

# ソースコードをコピーしてビルド
COPY . .
WORKDIR /app/Microservices/CompanyManagementService
RUN dotnet publish -c Release -o out

# ランタイムステージ
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/Microservices/CompanyManagementService/out ./
ENTRYPOINT ["dotnet", "CompanyManagementService.dll"]