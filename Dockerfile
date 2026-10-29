# Stage 1: Build the React client using Bun
FROM oven/bun:latest AS client-build
WORKDIR /src/client
COPY client/package.json client/bun.lockb* ./
RUN bun install
COPY client/ ./
RUN bun run build

# Stage 2: Build the .NET API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS server-build
WORKDIR /src
COPY server/API/API.csproj server/API/
COPY server/Infrastructure/Infrastructure.csproj server/Infrastructure/
RUN dotnet restore server/API/API.csproj

COPY server/ ./server/
WORKDIR /src/server/API
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Final Runtime Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=server-build /app/publish .
COPY --from=client-build /src/client/dist ./wwwroot

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]