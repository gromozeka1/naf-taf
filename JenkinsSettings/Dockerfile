FROM jenkins/jenkins:latest AS myj

# Disable SetUp wizard for Jenkins
ENV JAVA_OPTS -Djenkins.install.runSetupWizard=false
# Setup Jenkins Configuration As Code plugin settings path
ENV CASC_JENKINS_CONFIG /var/jenkins_home/casc.yaml

USER root

# Install plugins from plugins.txt
COPY plugins.txt /usr/share/jenkins/ref/plugins.txt
RUN jenkins-plugin-cli --plugins -f /usr/share/jenkins/ref/plugins.txt

# Copy settings for Jenkins Configuration As Code plugin
COPY casc.yaml /var/jenkins_home/casc.yaml

# Install PowerShell
RUN apt-get update \
    && apt-get install -y curl \
    && curl -sSL https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl -sSL https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/microsoft.list \
    && apt-get update \
    && apt-get install -y powershell

# Install .NET dependencies
RUN apt-get update && \
    apt-get install -y libunwind8 libicu-dev lsb-release

# Install .NET
RUN apt-get update && apt-get install -y wget
RUN wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh \
    && chmod +x ./dotnet-install.sh \
    && ./dotnet-install.sh --channel 7.0 --install-dir /usr/share/dotnet \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet

# Delete temp file
RUN rm dotnet-install.sh

USER jenkins