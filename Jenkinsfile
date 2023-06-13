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
  }
}