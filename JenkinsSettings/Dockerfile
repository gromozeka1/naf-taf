#docker build -t jenkins:myj .
#docker run --name jenkins --rm -p 8080:8080 --env JENKINS_ADMIN_ID=admin --env JENKINS_ADMIN_PASSWORD=password jenkins:myj

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

USER jenkins

#
## Dependency to run dotnet projects
#RUN apt-get update && \
    #apt-get install -y libunwind8 libicu-dev lsb-release
#
## Install docker CLI
#RUN apt-get update && apt-get install -y docker-ce-cli

