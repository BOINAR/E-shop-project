# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier uniquement le fichier csproj et restaurer les dépendances
COPY ./Server.csproj ./Server/
RUN dotnet restore ./Server/Server.csproj

# Copier tout le reste du projet après la restauration des dépendances
COPY . ./Server
WORKDIR /src/Server

# Construire le projet en mode Release et sortir les fichiers dans le répertoire /app/build
RUN dotnet build -c Release -o /app/build

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Exposer les ports 80 et 443 pour les connexions HTTP et HTTPS
EXPOSE 80
EXPOSE 443

# Copier les fichiers construits depuis le build stage
COPY --from=build /app/build .

# Lancer l'application via dotnet
ENTRYPOINT ["dotnet", "Server.dll"]

