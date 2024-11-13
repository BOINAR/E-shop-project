# Étape de build (pour construire l'application et exécuter des commandes comme 'dotnet ef')
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copier tous les fichiers pour garantir que le projet complet est accessible
COPY . . 

# Restaurer les dépendances
RUN dotnet restore Server.csproj

# Installer dotnet-ef globalement (outil pour les migrations)
RUN dotnet tool install --global dotnet-ef

# Ajouter le chemin permanent pour les outils globalement installés
ENV PATH="${PATH}:/root/.dotnet/tools"

# Construire le projet en mode Release et sortir les fichiers dans le répertoire /app/publish
RUN dotnet publish -c Release -o /app/publish

# Étape d'exécution - Utilisation du SDK .NET également pour l'exécution
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime
WORKDIR /app

# Copier les fichiers publiés depuis l'étape de build
COPY --from=build /app/publish .

# Exposer les ports 80 et 443 pour les connexions HTTP et HTTPS
EXPOSE 80
EXPOSE 443

# Lancer l'application via dotnet
ENTRYPOINT ["dotnet", "Server.dll"]