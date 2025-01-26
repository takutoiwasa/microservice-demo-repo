# ビルドステージ
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# プロジェクトファイルをコピーして依存関係を復元
COPY *.csproj ./
RUN dotnet restore

# ソースコードをコピーしてビルド
COPY . .
RUN dotnet publish -c Release -o out

# ランタイムステージ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

EXPOSE 5000

COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "ItemManagementService.dll"]