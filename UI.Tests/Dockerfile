# Install base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set working dir
WORKDIR /project

# Copy files
COPY ./Core ./Core
COPY ./UI.PageObject ./UI.PageObject
COPY ./UI.Tests ./UI.Tests

# Set working dir
WORKDIR /project/UI.Tests

# Restore and build project
RUN dotnet restore UI.Tests.csproj
RUN dotnet build --no-restore

# Install PowerShell
SHELL ["pwsh", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]
# Add Playwright and dependencies
RUN pwsh ./bin/Debug/net7.0/playwright.ps1 install --with-deps