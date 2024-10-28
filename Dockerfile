# Etapa de execução em ambiente de produção (utilizada no Heroku)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Configurar para escutar na porta dinâmica do Heroku
ENV ASPNETCORE_URLS=http://*:$PORT

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar e restaurar dependências
COPY ["Api/Api.csproj", "Api/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infra/Infra.csproj", "Infra/"]
RUN dotnet restore "Api/Api.csproj"

# Copiar todos os arquivos e compilar
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final - configuração do contêiner para execução no Heroku
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Entrada do contêiner para execução no Heroku
ENTRYPOINT ["dotnet", "Api.dll"]