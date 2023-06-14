pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        script {
          sh '''
                dotnet restore
                dotnet build
              '''
        }
      }
    }
    stage('Install browsers and dependencies'){
      steps {
        script{
          sh '''
                pwsh UI.Tests/bin/Debug/net7.0/playwright.ps1 install --with-deps
              '''
        }
      }
    }
    stage('Run tests'){
    steps {
        script{
          sh '''
                dotnet test --no-build --logger "trx;LogFileName=TestResults.trx"
              '''
        }
      }
    }
  }
}