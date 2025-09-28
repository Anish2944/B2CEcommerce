# ========================
# BUILD STAGE
# ========================
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore dependencies
RUN dotnet restore "./B2CEcommerceApp.csproj"

# Build in Release mode
RUN dotnet publish "./B2CEcommerceApp.csproj" -c Release -o /app/publish

# ========================
# RUNTIME STAGE
# ========================
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Set environment to Production
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose port 8080 (Render expects this)
EXPOSE 8080

# Tell ASP.NET Core to listen on port 8080
ENV ASPNETCORE_URLS=http://+:8080

# Run the app
ENTRYPOINT ["dotnet", "B2CEcommerceApp.dll"]
