pipeline {
    agent any
    environment {
        appName = "variable" 
    }
    stages {
        stage("Stage 1"){
           steps {
                script {			
                    echo 'hola mundo'
                }
            }
        }
    }
    post {
        always {
           echo 'always'
        }
        success {
            echo 'success'
        }
        failure {
            echo 'failure'
        }
    }
} 
