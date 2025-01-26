# ビルドステージ
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# ソリューションファイルとプロジェクトファイルをコピー
COPY BlazorFrontend.Client/*.csproj ./BlazorFrontend.Client/
RUN dotnet restore BlazorFrontend.Client/BlazorFrontend.Client.csproj

# ソースコードを全てコピーしてビルド
COPY . .
WORKDIR /app/BlazorFrontend.Client
RUN dotnet publish -c Release -o out

# ランタイムステージ
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/BlazorFrontend.Client/out ./
ENTRYPOINT ["dotnet", "BlazorFrontend.Client.dll"]