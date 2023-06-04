# Install base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set working dir
WORKDIR /project

# Copy files
COPY ./Core ./Core
COPY ./API.Tests ./API.Tests

# Set working dir
WORKDIR /project/API.Tests

# Restore and build project
RUN dotnet restore API.Tests.csproj
RUN dotnet build --no-restore