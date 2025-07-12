# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar archivos del proyecto
COPY . ./

# Restaurar dependencias
RUN dotnet restore

# Publicar el proyecto (modo Release)
RUN dotnet publish -c Release -o out

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

# Copiar app compilada desde la etapa anterior
COPY --from=build /app/out ./

# Expone el puerto (Railway usa este puerto por defecto)
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
EXPOSE 8080

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Backend.dll"]
