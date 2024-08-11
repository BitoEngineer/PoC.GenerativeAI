# Build the Docker image
# docker build -t insightsextractorapi .
# Run the Docker container
# docker run -d -p 5000:5000 --name insightsextractorapi-container insightsextractorapi
# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 443

# SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the entire solution
COPY . .

# Restore all dependencies for the entire solution
RUN dotnet restore "PoC.GenerativeAI.InsightsExtractorAPI/PoC.GenerativeAI.InsightsExtractorAPI.csproj"

# Build the specific project
WORKDIR "/src/PoC.GenerativeAI.InsightsExtractorAPI"
RUN dotnet build "PoC.GenerativeAI.InsightsExtractorAPI.csproj" -c Release -o /app/build

# Publish the specific project
RUN dotnet publish "PoC.GenerativeAI.InsightsExtractorAPI.csproj" -c Release -o /app/publish

# Final stage to create the runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PoC.GenerativeAI.InsightsExtractorAPI.dll"]