# ---------- Etapa 1: compilar e publicar ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia só os arquivos de projeto primeiro (o restore fica em cache)
COPY global.json ./
COPY StudyTube.Api/StudyTube.Api.csproj StudyTube.Api/
COPY StudyTube.Data/StudyTube.Data.csproj StudyTube.Data/
COPY StudyTube.Domain/StudyTube.Domain.csproj StudyTube.Domain/
COPY StudyTube.Services/StudyTube.Services.csproj StudyTube.Services/
RUN dotnet restore StudyTube.Api/StudyTube.Api.csproj

# Copia o resto do código e publica a API
COPY . .
RUN dotnet publish StudyTube.Api/StudyTube.Api.csproj -c Release -o /app/publish --no-restore

# ---------- Etapa 2: imagem final, só com o runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Pasta gravável para o banco SQLite (a imagem roda com o usuário "app", sem root)
RUN mkdir -p /app/data && chown -R $APP_UID:$APP_UID /app/data

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production \
    ConnectionStrings__Default="Data Source=/app/data/studytube.db"

USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "StudyTube.Api.dll"]
