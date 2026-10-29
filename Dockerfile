# Stage 1: Build the React client using Bun
FROM oven/bun:latest AS client-build
WORKDIR /src/client
COPY client/package.json client/bun.lockb* ./
RUN bun install
COPY client/ ./
RUN bun run build

# Stage 2: Build the .NET API
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS server-build
WORKDIR /src

# Copy ALL backend project files needed for restore
COPY server/API/API.csproj server/API/
COPY server/Infrastructure/Infrastructure.csproj server/Infrastructure/
COPY server/Service/Service.csproj server/Service/

# Restore dependencies
RUN dotnet restore server/API/API.csproj

# Copy the rest of the backend source code
COPY server/ ./server/
WORKDIR /src/server/API
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Final Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Copy the compiled .NET app
COPY --from=server-build /app/publish .
# Copy the compiled React app into the .NET static files directory
COPY --from=client-build /src/client/dist ./wwwroot

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]